namespace Invariants
{
    public static class FileSystem
    {

        public static readonly string[] CleanUpGlobPatterns =
        {
            GlobPattern.Bin,
            GlobPattern.Obj,
            GlobPattern.TestResults
        };

        public static class GlobPattern
        {
            public const string All = "*";
            public const string Bin = "**/bin";
            public const string Obj = "**/obj";
            public const string NugetFiles = "*.nupkg";
            public const string NugetSymbolFiles = "symbols.nupkg";
            public const string TestResults = "**/TestResults";
        }

        public static class Directory
        {
            public const string Artifacts = "artifacts";
            public const string Build = "build";
            public const string NuGet = "nuget";
            public const string Publish = "publish";
            public const string Src = "src";
        }

        public static class FileName
        {
            public const string Loggers = "trx;LogFileName=TestResult.xml";
        }
    }
}
