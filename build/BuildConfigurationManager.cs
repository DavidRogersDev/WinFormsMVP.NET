using System.Collections.Generic;

namespace _build
{
    public sealed class BuildConfigurationManager
    {
        readonly IReadOnlyCollection<Project> allProjects;
        readonly Solution solution;
        internal BuildConfigurationManager(Solution solution)
        {
            this.solution = solution;
            this.allProjects = solution.AllProjects;
        }

        public IReadOnlyCollection<Project> GetPackableProjects() => allProjects
            .Where(p => p != solution._build)
            .ToArray();

        public IReadOnlyCollection<TargetFrameworkProjection> GetTargetFrameworkProjections() => GetPackableProjects()
                .SelectMany(project => project.GetTargetFrameworks()
                    .Select(targetFramework => new TargetFrameworkProjection(project, targetFramework))
                ).ToArray();
    }

    public record TargetFrameworkProjection(Project Project, string TargetFramework);
}
