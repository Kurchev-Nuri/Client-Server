using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Russian.Post.Database.Configurations;
using Russian.Post.Database.Contexts;
using System;

namespace Russian.Post.Database.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRussianPostDataBase(this IServiceCollection services, Action<RussianPostDbConfiguration> action)
        {
            var options = new RussianPostDbConfiguration();

            action?.Invoke(options);
            ThrowIfOptionsIsEmty(options);

            services.AddDbContext<RussianPostContext>(opt => opt.UseSqlServer(options.ConnectionString));

            return services;
        }

        private static void ThrowIfOptionsIsEmty(RussianPostDbConfiguration options)
        {
            if (string.IsNullOrWhiteSpace(options.ConnectionString))
                throw new ArgumentNullException($"{nameof(options.ConnectionString)} is empty.");
        }
    }
}
