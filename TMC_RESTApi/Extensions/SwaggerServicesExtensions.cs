using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            var s = services.AddSwaggerGen(c =>
            {
                //  require to enter API key
                c.OperationFilter<APIKeyHeaderSwaggerAttribute>();
                //
                //
                ///index.html
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "TMCBackendAPI", Version = "v1" });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TMCBackendAPI",
                    Description = "TMCBackendAPI - simple API to access details about a group of users, providing the functionality of “User Management” for an admin page ",
                    TermsOfService = new Uri("http://www.siccolo.com/"),
                    Contact = new OpenApiContact
                    {
                        Name = "GD",
                        Email = "siccolo@gmail.com",
                        Url = new Uri("http://www.siccolo.com/"),
                    }
                });
                //
            });

            return s;
        }
    }

    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddSwaggerServices(this IApplicationBuilder app)
        {
            var a = app.UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        //https://localhost:44354/index.html
                        c.RoutePrefix = String.Empty;// "swagger/ui";
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TMCBackendAPI(v1)");
                    });

            return a;
        }
    }

    public class APIKeyHeaderSwaggerAttribute : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "API",
                In = ParameterLocation.Header,
                Required = true
            });
        }

    }
}
