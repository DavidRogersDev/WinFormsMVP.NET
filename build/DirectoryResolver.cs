public sealed class DirectoryResolver
{
    readonly AbsolutePath rootDirectory;
    public DirectoryResolver(AbsolutePath rootDirectory)
    {
        this.rootDirectory = rootDirectory ?? throw new ArgumentNullException(nameof(rootDirectory));
    }

    public AbsolutePath ArtifactsDirectory => rootDirectory / FileSystem.Directory.Artifacts;
    public AbsolutePath SourceDirectory => rootDirectory / FileSystem.Directory.Src;
}

