using System.Configuration;


public sealed class ReleaseGuard
{
    readonly GitVersion gitVersion;
    readonly PackagePublishConfig nugetOrg;
    readonly PackagePublishConfig githubPackages;

    internal ReleaseGuard(GitVersion GitVersion, PackagePublishConfig nugetOrg, PackagePublishConfig githubPackages)
    {
        this.githubPackages = githubPackages;
        this.nugetOrg = nugetOrg;
        gitVersion = GitVersion ?? throw new ArgumentNullException(nameof(GitVersion));
    }

    private bool IsPreReleaseBuild =>
        gitVersion.PreReleaseLabel.Equals(BuildType.Ci, StringComparison.OrdinalIgnoreCase) ||
        gitVersion.PreReleaseLabel.Equals(BuildType.Rc, StringComparison.OrdinalIgnoreCase);

    private bool IsReleaseOrMainBranch =>
        gitVersion.BranchName.StartsWith(Branch.Release, StringComparison.OrdinalIgnoreCase) ||
        gitVersion.BranchName.Equals(Branch.Main, StringComparison.OrdinalIgnoreCase);

    private bool IsReleaseBranch =>
        gitVersion.BranchName.StartsWith(Branch.Release, StringComparison.OrdinalIgnoreCase);

    public bool IsTaggedBuild => gitVersion.PreReleaseTag.IsEmpty();

    public bool BuildToBePacked(OverrideMode? overrideMode = null) => overrideMode switch
    {
        OverrideMode.Allow => true,
        OverrideMode.Deny => false,
        _ => IsReleaseOrMainBranch || IsTaggedBuild
    };

    public bool BuildToBeReleasedToNugetOrg() => IsTaggedBuild;

    public bool BuildToBeReleasedToGitHub() => IsPreReleaseBuild;

    public PackagePublishConfig ResolvePublishDestinationDetails()
    {
        if (BuildToBeReleasedToNugetOrg())
        {
            return nugetOrg;
        }
        else if (BuildToBeReleasedToGitHub())
        {
            return githubPackages;
        }
        else
        {
            return new PackagePublishConfig(string.Empty, string.Empty);
        }
    }
}

public record PackagePublishConfig(string Token, string Url)
{
    public bool HasNoValue => string.IsNullOrWhiteSpace(Token) || string.IsNullOrWhiteSpace(Url);
}
