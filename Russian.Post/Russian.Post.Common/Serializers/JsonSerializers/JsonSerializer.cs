using Newtonsoft.Json;
using Russian.Post.Common.Serializers.JsonExtension;

namespace Russian.Post.Common.Serializers.JsonSerializers
{
    internal sealed class JsonSerializer : ISerializer
    {
        readonly JsonSerializerSettings _settings = JsonSerializerHelper.DefaultSerializerSettings;

        public string Serialize<TInput>(TInput value) => JsonConvert.SerializeObject(value, _settings);

        public TResult Deserialize<TResult>(string value) => JsonConvert.DeserializeObject<TResult>(value, _settings);
    }
}
