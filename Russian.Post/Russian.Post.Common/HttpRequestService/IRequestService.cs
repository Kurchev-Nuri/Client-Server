using Russian.Post.Common.Results;
using System;
using System.Threading.Tasks;

namespace Russian.Post.Common.HttpRequestService
{
    public interface IRequestService
    {
        Task<PostResult<TResponse>> MakeGetRequest<TResponse>(Uri uri);

        Task<PostResult<TResponse>> MakeGetRequest<TRequest, TResponse>(Uri uri, TRequest parameters = default);

        Task<PostResult<TResponse>> MakePostRequest<TRequest, TResponse>(Uri uri, TRequest content = default);
    }
}
