#tool "dotnet:?package=GitVersion.Tool&version=5.11.1"

/**********************************************************************
 * Arguments
 **********************************************************************/
// Build Configuration
var buildConfiguration = "Release";
var configuration = Argument("configuration", buildConfiguration);

// Task Names
var defaultTask = "Default";
var cleanTask = "Clean";
var restoreTask = "Restore";
var versionTask = "Version";
var buildTask = "Build";
var packTask = "Pack";
var nugetPushTask = "NugetPush";

// Build Targets
var target = Argument("target", defaultTask);
var releaseNotes = Argument("releaseNotes", "NA");

/**********************************************************************
 * Global Variables
 **********************************************************************/
// versioning stuff
var version = string.Empty;
var informationalVersion = string.Empty;
var majorMinorPatch = string.Empty;
var assemblyVersion = string.Empty;
var commit = string.Empty;
var fileVersion = string.Empty;
bool isPublishBranch = false;

var artifactsRelativePath = "./artifacts";
var nugetFiles = "./*.nupkg";
var nugetFilesPath = artifactsRelativePath + "/" + nugetFiles;
var projFileGlob = "./src/**/*.csproj";

// Nuget Server Settings
var variableMissing = "missing";
var nugetApiKey = EnvironmentVariable("NUGET_API_KEY") ?? variableMissing;
var nugetServer = EnvironmentVariable("NUGET_URL") ?? variableMissing;

/**********************************************************************
 * Artifacts
 **********************************************************************/

// Artifacts Directory
EnsureDirectoryExists(artifactsRelativePath);
var artifactsDirectory = Directory(artifactsRelativePath);
var artifactsDirectoryAsString = artifactsDirectory.Path.GetDirectoryName();

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task(cleanTask)
    .Does(() =>
{    
	Information($"Cleaning directories ...");
	CleanDirectories(artifactsDirectoryAsString);
	CleanDirectories("./**/obj");
	CleanDirectories("./**/bin");
});


Task(restoreTask)    
    .Description("Restoring the solution dependencies")
    .Does(() => {
        var projects = GetFiles(projFileGlob);

        foreach(var projFile in projects )
        {
            DotNetRestore(projFile.ToString());
        } 
    });

Task(versionTask)    
    .Description("Stamping with versioning ...")
    .Does(() => {
        try
        {
            var gitVersionInfo = GitVersion(new GitVersionSettings {                
                UpdateAssemblyInfo = true
            });

            // Package Version
            version = gitVersionInfo.NuGetVersionV2;
            informationalVersion = gitVersionInfo.InformationalVersion;
            majorMinorPatch = gitVersionInfo.MajorMinorPatch;
            assemblyVersion = gitVersionInfo.AssemblySemVer;
            fileVersion = gitVersionInfo.AssemblySemFileVer;
            commit = gitVersionInfo.Sha;
            isPublishBranch = gitVersionInfo.BranchName.StartsWith("publish/") || gitVersionInfo.BranchName.Equals("main");

            Information($"Building version {version.ToString()}");
            Information($"BranchName {gitVersionInfo.BranchName.ToString()}");
            Information($"isPublishBranch {gitVersionInfo.BranchName.StartsWith("publish/").ToString()}");
        }
        catch
        {
            Error("Could not set GitVersionInfo");
        }        
});

Task(buildTask)
    .IsDependentOn(versionTask)
    .IsDependentOn(cleanTask)    
    .Does(() =>
{
     var buildSettings = new DotNetBuildSettings {
                        Configuration = configuration,
                        MSBuildSettings = new DotNetMSBuildSettings()
                                                      .WithProperty("Version", version)
                                                      .WithProperty("Copyright", $"Copyright David Rogers {DateTime.Now.Year}")
                                                      .WithProperty("AssemblyVersion", assemblyVersion)
                                                      .WithProperty("FileVersion", fileVersion)
                                                      .WithProperty("InformationalVersion", informationalVersion)
                       };
     
	 var projects = GetFiles(projFileGlob);
     
	 foreach(var projFile in projects )
     {
         Information($"Building {projFile.ToString()}");
         DotNetBuild(projFile.ToString(), buildSettings);
     }
});

Task(packTask)
 .WithCriteria(isPublishBranch)
 .IsDependentOn(buildTask)     
 .Does(() => {    
	
	 var projects = GetFiles(projFileGlob);
     
	 foreach(var projFile in projects )
     {
        var settings = new DotNetPackSettings
        {
            ArgumentCustomization = args => args
                            .Append($"/p:RepositoryType=git")
                            .Append($"/p:RepositoryBranch=main")
                            .Append($"/p:RepositoryUrl=https://github.com/DavidRogersDev/WinFormsMVP.NET.git")
                            .Append($"/p:RepositoryCommit={commit}")
                            .Append($"/p:PackageReleaseNotes=\"{releaseNotes}\"")
                            .Append($"/p:PackageVersion={version}"),
            Configuration = configuration,
            OutputDirectory = artifactsDirectory,
            NoBuild = true,
            NoRestore = true,        
            MSBuildSettings = new DotNetMSBuildSettings()                        
                            .WithProperty("PackageVersion", version)
                            .WithProperty("Version", version)
                            .WithProperty("AssemblyVersion", assemblyVersion)
                            .WithProperty("InformationalVersion", informationalVersion)
        };            

        DotNetPack(projFile.ToString(), settings);		          
     }	
 });

Task(nugetPushTask)
 .WithCriteria(isPublishBranch)
 .IsDependentOn(packTask)     
 .Does(() => {    

    if(nugetApiKey == variableMissing) {
        throw new Exception("Nuget Api Key not properly set.");
    }

    if(nugetServer == variableMissing) {
        throw new Exception("Nuget Server Url not properly set.");
    }

    foreach(var file in GetFiles(nugetFilesPath)) {
        DotNetNuGetPush(file, new DotNetNuGetPushSettings {
            ApiKey = nugetApiKey,
            Source = nugetServer
        });
    }

 }); 


/********************************************************************
 * Default Task
 *******************************************************************/
Task(defaultTask)
    .IsDependentOn(cleanTask)
    .IsDependentOn(restoreTask)
	.IsDependentOn(versionTask)
	.IsDependentOn(buildTask)
	.IsDependentOn(packTask)
	.IsDependentOn(nugetPushTask);

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
