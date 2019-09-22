using Russian.Post.Common.Results;
using Russian.Post.Common.Serializers;
using Russian.Post.Consts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Russian.Post.Common.HttpRequestService
{
    internal sealed class RequestService : IRequestService
    {
        private readonly HttpClient _client;
        private readonly ISerializer _serializer;

        public RequestService(HttpClient client, ISerializer serializer)
        {
            _client = client;
            _serializer = serializer;
        }

        public Task<PostResult<TResponse>> MakeGetRequest<TResponse>(string uri) => MakeRequestPure<string, TResponse>(uri, HttpMethod.Get);

        public Task<PostResult<TResponse>> MakePostRequest<TRequest, TResponse>(string uri, TRequest content = default)
        {
            return MakeRequestPure<TRequest, TResponse>(uri, HttpMethod.Post, content);
        }

        private async Task<PostResult<TResponse>> MakeRequestPure<TRequest, TResponse>(string uri, HttpMethod method, TRequest content = default)
        {
            using (var request = new HttpRequestMessage(method, uri))
            {
                FillBody(request, content);

                try
                {
                    using (var response = await _client.SendAsync(request))
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            return PostResult<TResponse>.WithError(PostErrorCodes.RemoteRequestIsFailed);

                        var result = await response.Content.ReadAsStringAsync();
                        return _serializer.Deserialize<PostResult<TResponse>>(result);
                    }
                }
                catch (Exception)
                {
                    return PostResult<TResponse>.WithError(PostErrorCodes.RemoteRequestIsFailed);
                }
            }
        }

        private void FillBody<T>(HttpRequestMessage request, T content)
        {
            if (EqualityComparer<T>.Default.Equals(content, default))
                return;

            request.Content = new StringContent(_serializer.Serialize(content), Encoding.UTF8, MimeTypeConsts.ApplicationJson);
        }
    }
}
