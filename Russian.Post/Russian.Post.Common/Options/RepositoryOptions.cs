namespace Russian.Post.Common.Options
{
    public sealed class RepositoryOptions
    {
        public bool AutoSaveEnabled { get; set; } = true;

        public static RepositoryOptions Default => new RepositoryOptions();
    }
}
