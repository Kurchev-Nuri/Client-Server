using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Russian.Post.Forms.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddFormValidators(this IServiceCollection services)
        {
            return services.Scan(u => u.FromAssemblies(typeof(ServiceExtensions).Assembly)
                .AddClasses(v => v.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()
            );
        }
    }
}
