namespace Russian.Post.Common.Serializers
{
    public interface ISerializer
    {
        string Serialize<TInput>(TInput value);

        TResult Deserialize<TResult>(string value);
    }
}
