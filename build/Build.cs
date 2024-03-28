using System;
using System.Linq;
using System.Numerics;
using Invariants;
using Nuke.Common;
using Nuke.Common.ChangeLog;
using Nuke.Common.CI;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    // Build Configs
    public static int Main() => Execute<Build>(x => x.Push);

    private Target CleanAndBuild => _ => _
    .Description(Description.LocalBuild)
    .DependsOn(Print, Clean, Restore, Compile);

    private Target CleanBuildAndPack => _ => _
    .Description(Description.LocalBuildAndPack)
    .DependsOn(CleanAndBuild, Pack);

    // Parameters and Environment Variables
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;
    [GitVersion]
    readonly GitVersion GitVersion;
    [Solution(GenerateProjects = true)]
    readonly Solution Solution;
    [Parameter]
    [Secret]
    readonly string NUGET_API_KEY;
    [Parameter]
    [Secret]
    readonly string PACKAGES_GITHUB_NUGET_PAT;
    [Parameter]
    readonly string NUGET_URL;
    [Parameter]
    readonly string PACKAGES_GITHUB_NUGET_URL;
    [Parameter]
    readonly string RELEASE_NOTES;
    [Parameter]
    readonly bool IgnoreFailedSources = false;
    bool WantToPackThisBuild;

    // Global Variables
    private static AbsolutePath SourceDirectory => RootDirectory / FileSystem.Directory.Src;
    private static AbsolutePath ArtifactsDirectory => RootDirectory / FileSystem.Directory.Artifacts;

    Target Print => _ => _
    .Description(Description.Print)
    .DependentFor(Clean)
    .Executes(() =>
    {
        Log.Information(LogMessage.ReleaseNotes, RELEASE_NOTES);
        Log.Information("Major Minor Patch = {Value}", GitVersion.MajorMinorPatch); // TODO
        Log.Information("NuGet Version = {Value}", GitVersion.NuGetVersion); // TODO
        Log.Information("PreReleaseLabel = {Value}", GitVersion?.PreReleaseLabel ?? "????"); // TODO

        if (IsLocalBuild)
        {
            Log.Information(LogMessage.Configuration, Configuration);
            Log.Information("Root Directory = {Value}", RootDirectory); // TODO
            Log.Information("Source Directory = {Value}", SourceDirectory); // TODO
        }

        WantToPackThisBuild = GitVersion.BranchName.StartsWith(Branch.Release, StringComparison.OrdinalIgnoreCase)
            || GitVersion.BranchName.Equals(Branch.Main, StringComparison.OrdinalIgnoreCase);

        ArtifactsDirectory.CreateOrCleanDirectory();
    });

    Target Clean => _ => _
        .Unlisted()
        .Description(Description.Clean)
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories(FileSystem.CleanUpGlobPatterns).DeleteDirectories();
        });

    Target Restore => _ => _
    .Description(Description.Restore)
    .DependsOn(Clean)
    .Executes(() =>
    {        
        DotNetRestore(_ => _
        .SetProjectFile(Solution)
        .SetIgnoreFailedSources(IgnoreFailedSources)
        );
    });

    Target Compile => _ => _
        .Description(Description.Compile)
        .DependsOn(Restore)
        .Executes(() =>
        {
            var publishConfiguration =
                Solution.AllProjects
                .Where(p => p != Solution._build)
                .SelectMany(project => project.GetTargetFrameworks().Select(targetFramework => new
                {
                    Project = project,
                    Framework = targetFramework
                })).ToList();

            DotNetPublish(_ =>
                _.SetProject(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .SetCopyright(PackageValue.Copyright)
                .SetAssemblyVersion(GitVersion.AssemblySemFileVer)
                .SetFileVersion(GitVersion.MajorMinorPatch)
                .SetVersion(GitVersion.NuGetVersion)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .CombineWith(publishConfiguration, (_, v) =>
                    _.SetProject(v.Project)
                    .AddNoWarns(NoWarn.VariableUnused)
                    .SetFramework(v.Framework))
                );
        });

    Target Pack => _ => _
    .Description(Description.Pack)
    .DependsOn(Compile)
    .OnlyWhenDynamic(() => WantToPackThisBuild) 
    .Executes(() =>
    {
        var packableProjects = Solution.AllProjects
            .Where(p => p != Solution._build)
            .ToList();

        packableProjects.ForEach(p =>
        {
            DotNetPack(s => s
                    .SetProject(p)
                    .SetConfiguration(Configuration)
                    .EnableNoBuild()
                    .EnableNoRestore()
                    .SetNoDependencies(false)
                    .SetVersion(GitVersion.NuGetVersion)
                    .SetRepositoryType(AzurePipelinesRepositoryType.Git.ToString().ToLowerInvariant())
                    .SetPackageReleaseNotes(RELEASE_NOTES)
                    .SetAuthors(PackageValue.Author)
                    .SetPackageRequireLicenseAcceptance(false)
                    .SetProperty(PackageProperty.RepositoryBranch, GitVersion.BranchName)
                    .SetProperty(PackageProperty.RepositoryCommit, GitVersion.Sha)
                    .SetProperty(PackageProperty.Copyright, PackageValue.Copyright)
                    .SetProperty(PackageProperty.PackageIcon, PackageValue.Icon)
                    .SetOutputDirectory(ArtifactsDirectory)
                    );
        });
    });

    Target Push => _ => _
        .Description(Description.Push)
        .OnlyWhenStatic(() => IsServerBuild) // checked before the build steps run.
        .OnlyWhenStatic(() => Configuration.Equals(Configuration.Release))
        .OnlyWhenDynamic(() => WantToPackThisBuild)
        .Requires(() => NUGET_API_KEY)
        .Requires(() => NUGET_URL)
        .Requires(() => PACKAGES_GITHUB_NUGET_PAT)
        .Requires(() => PACKAGES_GITHUB_NUGET_URL)
        .DependsOn(Pack)
        .Executes(() =>
        {
            var nugetFiles = ArtifactsDirectory.GlobFiles(FileSystem.GlobPattern.NugetFiles);

            Assert.NotEmpty(nugetFiles, "There are no Nuget files");

            var branchName = GitVersion.BranchName;

            // if we are on the main branch and it is not a pre-release, publish to Nuget.org
            if (branchName.Equals(Branch.Main, StringComparison.OrdinalIgnoreCase)
                && string.IsNullOrWhiteSpace(GitVersion.PreReleaseLabel))
            {
                nugetFiles.Where(x => !x.Name.EndsWith(FileSystem.GlobPattern.NugetSymbolFiles))
                    .ForEach(x =>
                    {
                        DotNetNuGetPush(s => s
                            .SetTargetPath(x)
                            .SetSource(NUGET_URL)
                            .SetApiKey(NUGET_API_KEY)
                        );
                    });
            }

            nugetFiles.Where(x => !x.Name.EndsWith(FileSystem.GlobPattern.NugetSymbolFiles))
                .ForEach(x =>
                {
                    DotNetNuGetPush(s => s
                        .SetTargetPath(x)
                        .SetSource(PACKAGES_GITHUB_NUGET_URL)
                        .SetApiKey(PACKAGES_GITHUB_NUGET_PAT)
                    );
                });
        });

}
