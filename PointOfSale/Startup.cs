using AutoMapper;
using ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.DataSeed;
using System.Collections.Generic;
using System.Globalization;

namespace PointOfSale
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCords();
            services.ConfigureDbContext(Configuration);
            services.IdentityConfig();
            services.IdentityConfigModeFree();
            services.AuthServiceConfig(Configuration);
            services.WebServices();
            services.SwaggerDoc();
            services.AddAutoMapper();
            services.AddMvc();
        }


        private static void ConfigureJson(MvcJsonOptions obj) => obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DataSeed.SeedRoles(app);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Point of sale v1");
            });
            app.UseAuthentication();
            app.UseCors("AllowSpecificOrigin");
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
