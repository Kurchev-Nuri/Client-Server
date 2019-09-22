using Russian.Post.Common.Results;
using System;
using System.Threading.Tasks;

namespace Russian.Post.Common.HttpRequestService
{
    public interface IRequestService
    {
        Task<PostResult<TResponse>> MakeGetRequest<TResponse>(string uri);

        Task<PostResult<TResponse>> MakePostRequest<TRequest, TResponse>(string uri, TRequest content = default);
    }
}
