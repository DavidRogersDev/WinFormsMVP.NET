using _build;
using Nuke.Common;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    public static int Main() => IsLocalBuild
            ? Execute<Build>(t => t.Pack)
            : Execute<Build>(t => t.Push);

    protected override void OnBuildInitialized()
    {
        base.OnBuildInitialized();

        _releaseGuard = new ReleaseGuard(GitVersion, 
            new PackagePublishConfig(NUGET_API_KEY, NUGET_URL),
            new PackagePublishConfig(PACKAGES_GITHUB_NUGET_PAT, PACKAGES_GITHUB_NUGET_URL));
        _directoryResolver = new DirectoryResolver(RootDirectory);
        _buildConfigurationManager = new BuildConfigurationManager(Solution);

        Log.Information(LogMessage.IsTaggedBuild, _releaseGuard.IsTaggedBuild);
        Log.Information(LogMessage.MajorMinorPatch, GitVersion.MajorMinorPatch);
        Log.Information(LogMessage.NuGetVersion, GitVersion.NuGetVersion);
        Log.Information(LogMessage.PreReleaseLabel, GitVersion?.PreReleaseLabel ?? LogMessage.NoReleaseLabel);
        Log.Information(LogMessage.PreReleaseTag, GitVersion?.PreReleaseTag ?? LogMessage.NoReleaseTag);

        if (IsLocalBuild)
        {
            Log.Information(LogMessage.Configuration, Configuration);
            Log.Information(LogMessage.ReleaseNotes, RELEASE_NOTES);
            Log.Information(LogMessage.RootDirectory, RootDirectory);
            Log.Information(LogMessage.SourceDirectory, _directoryResolver.SourceDirectory);
        }

        _directoryResolver.ArtifactsDirectory.CreateOrCleanDirectory();
    }

    // Parameters
    [Parameter][Secret] readonly string NUGET_API_KEY;
    [Parameter][Secret] readonly string PACKAGES_GITHUB_NUGET_PAT;
    [Parameter] readonly string NUGET_URL;
    [Parameter] readonly string PACKAGES_GITHUB_NUGET_URL;
    [Parameter] readonly string RELEASE_NOTES;
    [Parameter] readonly bool IgnoreFailedSources = false;
    [Parameter] readonly bool LocalPack = false;
    [Parameter] readonly bool LocalPackDenyRelease = false;
    [Parameter(Param.Description.Configuration)]
    readonly Configuration Configuration = IsLocalBuild
        ? Configuration.Debug
        : Configuration.Release;

    // Environment Variables
    [GitVersion] readonly GitVersion GitVersion;
    [Solution(GenerateProjects = true)] readonly Solution Solution;

    // Fields
    DirectoryResolver _directoryResolver;
    ReleaseGuard _releaseGuard;
    BuildConfigurationManager _buildConfigurationManager;

    /********************************** Delegate Tasks **********************************/
    Target Clean => _ => _
        .Unlisted()
        .Description(Description.Clean)
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() => _directoryResolver.SourceDirectory.GlobDirectories(FileSystem.CleanUpGlobPatterns).DeleteDirectories());

    Target Restore => _ => _
    .Description(Description.Restore)
    .DependsOn(Clean)
    .Executes(() => DotNetRestore(_ => _
        .SetProjectFile(Solution)
        .SetIgnoreFailedSources(IgnoreFailedSources)
        ));

    Target Compile => _ => _
        .Description(Description.Compile)
        .DependsOn(Restore)
        .Executes(() =>
        {
            var publishConfiguration = _buildConfigurationManager.GetTargetFrameworkProjections();

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
                    .SetFramework(v.TargetFramework))
                );
        });

    Target Pack => _ => _
    .Description(Description.Pack)
    .DependsOn(Compile)
    .OnlyWhenDynamic(() => LocalPack
        ? _releaseGuard.BuildToBePacked(System.Configuration.OverrideMode.Allow)
        : LocalPackDenyRelease
            ? _releaseGuard.BuildToBePacked(System.Configuration.OverrideMode.Deny)
            : _releaseGuard.BuildToBePacked())
    .Executes(() =>
    {
        var packableProjects = _buildConfigurationManager.GetPackableProjects();

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
                    .SetOutputDirectory(_directoryResolver.ArtifactsDirectory)
                    );
        });
    });

    Target Push => _ => _
        .Description(Description.Push)
        .OnlyWhenStatic(() => IsServerBuild)
        .OnlyWhenStatic(() => Configuration.Equals(Configuration.Release))
        .OnlyWhenDynamic(() => _releaseGuard.BuildToBePacked())
        .Requires(() => NUGET_API_KEY)
        .Requires(() => NUGET_URL)
        .Requires(() => PACKAGES_GITHUB_NUGET_PAT)
        .Requires(() => PACKAGES_GITHUB_NUGET_URL)
        .DependsOn(Pack)
        .Executes(() =>
        {
            var nugetFiles = _directoryResolver.ArtifactsDirectory.GlobFiles(FileSystem.GlobPattern.NugetFiles)
                .Where(x => !x.Name.EndsWith(FileSystem.GlobPattern.NugetSymbolFiles));

            Assert.True(nugetFiles.Any(), LogMessage.Assertion.NoNugetFilesExist);

            PackagePublishConfig record = _releaseGuard.ResolvePublishDestinationDetails();

            if (record.HasNoValue)
            {
                Log.Information(LogMessage.NothingPushed);
            }
            else
            {
                nugetFiles.ForEach(x =>
                {
                    DotNetNuGetPush(s => s
                        .SetTargetPath(x)
                        .SetSource(record.Url)
                        .SetApiKey(record.Token)
                    );
                });
            }
        });


    /********************************** Ad-hoc Build Configs **********************************/
    Target CleanAndBuild => _ => _
        .Description(Description.CleanAndBuild)
        .DependsOn(Compile);
}
