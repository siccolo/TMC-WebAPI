using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using System.IO;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Extensions
{
    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddAppErrorHandling(this IApplicationBuilder app)
        {
            var a = app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;
                    var exceptionInfo = exception.Message;
                    var result = System.Text.Json.JsonSerializer.Serialize(new { error = "Exception occurred" });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                });
            });

            return a;
        }
    }
}
