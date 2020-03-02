using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection EnableCORS(this IServiceCollection services, IConfiguration configuration)
        {
            var s = services.AddCors();
            return s;
        }
    }


    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddCORSPolicy(this IApplicationBuilder app)
        {
            var a = app.UseCors(builder =>
                         builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader() );
            return a;
        }
    }
}
