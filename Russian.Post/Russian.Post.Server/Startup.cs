using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Russian.Post.Consts;
using Russian.Post.Database.Extensions;

namespace Russian.Post.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
            })
             .AddJsonOptions(options =>
             {
                 options.SerializerSettings.Formatting = Formatting.None;
                 options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                 options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                 options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
             })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //
            services.AddOptions();

            //
            services.AddRussianPostDataBase(opt =>
            {
                opt.ConnectionString = Configuration.GetConnectionString(DatabaseConsts.PostConnectionString);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
