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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Extensions;

namespace TMC_RESTApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.EnableCORS(this.Configuration);
            services.AddSwaggerServices();

            services.AddAPIKeyAuthentication(this.Configuration, "APIKey");

            services.AddTMCServices(this.Configuration, "Db");

            //The API should return “unpretty” aka (condensed, minified) JSON. -> By default, JSON is minified. 
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.AddCORSPolicy();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.AddAppErrorHandling();
                app.AddSwaggerServices();

                //populate datastorage
                app.PopulateTMCStorage();
            }
            else
            {
                app.AddAppErrorHandling();
                //populate datastorage
                app.PopulateTMCStorage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
