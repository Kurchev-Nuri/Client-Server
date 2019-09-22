using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Russian.Post.Business.Logic.Services.ClientMessages;
using Russian.Post.Client.Options;
using Russian.Post.Client.Workers;
using Russian.Post.Common.BackgroundProcessing;
using Russian.Post.Consts;
using System;

namespace Russian.Post.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();
            var services = new ServiceCollection();

            startup.ConfigureServices(services);

            using (var provider = services.BuildServiceProvider())
            {
                string connection = startup.Configuration.GetConnectionString(DatabaseConsts.HangfireConnectionString);
                var sqlStorage = new SqlServerStorage(connection, new SqlServerStorageOptions
                {
                    SchemaName = DatabaseConsts.HangfireSchemaName
                });

                GlobalConfiguration.Configuration.UseStorage(sqlStorage);
                GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(provider));

                var options = new BackgroundJobServerOptions { WorkerCount = 1 };

                using (var server = new BackgroundJobServer(options))
                {
                    HandleRecurringJob(provider);
                    Console.WriteLine("Client started!");

                    provider.GetRequiredService<ClientMessageWorker>()
                        .DoWork();
                }
            }
        }

        private static void HandleRecurringJob(ServiceProvider provider)
        {
            provider.GetRequiredService<IBackgroundProcessor>()
                .RecurrentJob<IClientMessagesService>(u => u.HandlePendingMessages());
        }
    }
}
