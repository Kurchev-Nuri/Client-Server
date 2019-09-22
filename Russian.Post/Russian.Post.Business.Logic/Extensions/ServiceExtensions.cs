using Microsoft.Extensions.DependencyInjection;

namespace Russian.Post.Business.Logic.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPostBusinessLogic(this IServiceCollection services)
        {
            return services;
        }
    }
}
