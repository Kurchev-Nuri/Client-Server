using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Russian.Post.Consts;
using System;
using System.IO;

namespace Russian.Post.Database.DesignTimeContextFactories.BaseFactories
{
    public abstract class BaseDesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
           where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable(CommonConsts.ASPNETCORE_ENVIRONMENT);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

            return BuildContext(configuration);
        }

        protected abstract TContext BuildContext(IConfigurationRoot configuration);
    }
}
