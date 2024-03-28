namespace Invariants
{
    public static class Description
    {
        public const string Clean = "Cleans the project.";
        public const string Compile = "Compiles the project.";
        public const string CleanAndBuild = "Cleans, restores Nuget packages and compiles the projects";
        public const string CleanBuildAndPack = "Cleans, restores Nuget packages, compiles the projects and packs them into Nuget packages";
        public const string Pack = "Packs the project into a Nuget package.";
        public const string Print = "Displays certain variables of interest to the console";
        public const string Push = "Pushes the packages to the package registry.";
        public const string Restore = "Restoring Project Dependencies.";
        public const string Test = "Executes tests.";
    }
}
