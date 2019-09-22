using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Russian.Post.Business.Logic.Configurations.Mapper;
using Russian.Post.Business.Logic.Extensions;
using Russian.Post.Client.Workers;
using Russian.Post.Common.Extensions;
using Russian.Post.Common.Options;
using Russian.Post.Consts;
using Russian.Post.Database.Extensions;
using Russian.Post.Forms.Extensions;
using System.IO;

namespace Russian.Post.Client
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //
            services.AddOptions();
            services.AddAutoMapper(config => config.AddProfile<RussianPostModelsProfile>(), typeof(RussianPostModelsProfile).Assembly);

            //
            services.AddRussianPostDataBase(opt =>
            {
                opt.ConnectionString = Configuration.GetConnectionString(DatabaseConsts.PostConnectionString);
            });

            //
            services.AddFormValidators();
            services.AddPostCommonSrevices();
            services.AddPostClientBusinessLogic();

            //
            services.AddScoped<ClientMessageWorker>();

            //
            services.Configure<ApiEndpointOptions>(Configuration.GetSection(nameof(ApiEndpointOptions)));
        }
    }
}
