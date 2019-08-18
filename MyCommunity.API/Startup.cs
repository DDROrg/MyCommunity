using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MyCommunity.DAL;

namespace MyCommunity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        private readonly ILogger _logger;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //===============================================
            //==Repository config============================
            var config = new ServerConfig();
            Configuration.Bind(config);
            //_logger.LogInformation("config.MongoDB : " + config.MongoDB.ConnectionString);
            var cmnContext = new CommunityContext(config.MongoDB);
            var cmnRepo = new CommunityRepository(cmnContext);
            services.AddSingleton<ICommunityRepository>(cmnRepo);

            //==Seed config==================================
            var cmnSeeder = new CommunitySeeder(cmnRepo);
            services.AddSingleton<ISeed>(cmnSeeder);

            //==MVC==========================================
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //===Auto Mapper Configurations==================
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var cmnSeeder = serviceScope.ServiceProvider.GetService<ISeed>();
                cmnSeeder.Seed();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
