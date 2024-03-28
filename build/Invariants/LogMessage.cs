namespace Invariants
{
    public static class LogMessage
    {
        public const string Configuration = "Configuration = {Value}";
        public const string IsReleaseBuild = "IsReleaseBuild = {Value}";
        public const string GitVersion = "GitVersion = {Value}";
        public const string ProjectsToRelease = "ProjectsToRelease = {Value}";
        public const string ReleaseNotes = "Release Notes = {Value}";
        public const string RootDirectory = "Root Directory = {Value}";
        public const string SourceDirectory = "Source Directory = {Value}";

        public const string IsTaggedBuild = "IsTaggedBuild = {Value}";
        public const string MajorMinorPatch = "Major Minor Patch = {Value}";
        public const string NuGetVersion = "NuGet Version = {Value}";
        public const string PreReleaseLabel = "Pre-Release Label = {Value}";
        public const string PreReleaseTag = "Pre-Release Tag = {Value}";

        public const string NoReleaseLabel = "No Release label";
        public const string NoReleaseTag = "No Release Tag";
        public const string NothingPushed = "Nothing pushed";

        public class Assertion
        {
            public const string NoNugetFilesExist = "There are no Nuget files";
        }
    }
}
