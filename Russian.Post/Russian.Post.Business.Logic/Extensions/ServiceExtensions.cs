using Microsoft.Extensions.DependencyInjection;
using Russian.Post.Business.Logic.Repositories.ClientMessages;
using Russian.Post.Business.Logic.Repositories.ServerMessages;
using Russian.Post.Business.Logic.Services.ClientMessages;
using Russian.Post.Business.Logic.Services.ServerMessages;

namespace Russian.Post.Business.Logic.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPostServerBusinessLogic(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IServerMessagesService, ServerMessagesService>();
            services.AddScoped<IServerMessagesRepository, ServerMessagesRepository>();

            return services;
        }

        public static IServiceCollection AddPostClientBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IClientMessagesService, ClientMessagesService>();
            services.AddScoped<IClientMessagesRepository, ClientMessagesRepository>();

            return services;
        }
    }
}
