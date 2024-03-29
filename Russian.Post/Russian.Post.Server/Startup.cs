﻿using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Russian.Post.Business.Logic.Configurations.Mapper;
using Russian.Post.Business.Logic.Extensions;
using Russian.Post.Common.Extensions;
using Russian.Post.Consts;
using Russian.Post.Database.Extensions;
using Russian.Post.Forms.Extensions;

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
            services.AddAutoMapper(config => config.AddProfile<RussianPostModelsProfile>(), typeof(RussianPostModelsProfile).Assembly);

            //
            services.AddRussianPostDataBase(opt =>
            {
                opt.ConnectionString = Configuration.GetConnectionString(DatabaseConsts.PostConnectionString);
            });

            //
            services.AddFormValidators();
            services.AddPostCommonSrevices();
            services.AddPostServerBusinessLogic();

            //
            services.AddHangfire(opt => opt.UseSqlServerStorage(Configuration.GetConnectionString(DatabaseConsts.HangfireConnectionString)));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            //
            app.UseMvcWithDefaultRoute();
        }
    }
}
