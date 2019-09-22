using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading;

namespace Russian.Post.Common.Serializers.JsonExtension
{
    public static class JsonSerializerHelper
    {
        static readonly Lazy<JsonSerializerSettings> _lazy;

        static JsonSerializerHelper()
        {
            _lazy = new Lazy<JsonSerializerSettings>(() => InitDefaultSettings(), LazyThreadSafetyMode.PublicationOnly);
        }

        public static JsonSerializerSettings DefaultSerializerSettings => _lazy.Value;

        static JsonSerializerSettings InitDefaultSettings()
        {
            return new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}
