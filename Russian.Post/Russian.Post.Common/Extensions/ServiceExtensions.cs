using Microsoft.Extensions.DependencyInjection;
using Russian.Post.Common.BackgroundProcessing;
using Russian.Post.Common.Validation.Factories;
using Russian.Post.Common.Validation.FluentValidator;

namespace Russian.Post.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPostCommonSrevices(this IServiceCollection services)
        {
            services.AddSingleton<IValidatorsFactory, ValidatorsFactory>();
            services.AddSingleton<IFluentClientValidator, FluentClientValidator>();
            services.AddSingleton<IBackgroundProcessor, HangfireBackgroundProcessor>();

            return services;
        }
    }
}
