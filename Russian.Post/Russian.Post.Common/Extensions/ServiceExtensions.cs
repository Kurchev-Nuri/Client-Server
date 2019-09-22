using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Russian.Post.Common.BackgroundProcessing;
using Russian.Post.Common.HttpRequestService;
using Russian.Post.Common.Serializers;
using Russian.Post.Common.Serializers.JsonSerializers;
using Russian.Post.Common.Validation.Factories;
using Russian.Post.Common.Validation.FluentValidator;
using Russian.Post.Consts;
using System;

namespace Russian.Post.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPostCommonSrevices(this IServiceCollection services)
        {
            services.AddOptionValidators();

            services.AddSingleton<ISerializer, JsonSerializer>();
            services.AddSingleton<IValidatorsFactory, ValidatorsFactory>();
            services.AddSingleton<IFluentClientValidator, FluentClientValidator>();
            services.AddSingleton<IBackgroundProcessor, HangfireBackgroundProcessor>();

            services.AddHttpClient<IRequestService, RequestService>().ConfigureHttpClient(u =>
            {
                u.BaseAddress = new Uri(CommonConsts.EndpointHost);
            });

            return services;
        }

        public static IServiceCollection AddOptionValidators(this IServiceCollection services)
        {
            return services.Scan(u => u.FromAssemblies(typeof(ServiceExtensions).Assembly)
                .AddClasses(v => v.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()
            );
        }
    }
}
