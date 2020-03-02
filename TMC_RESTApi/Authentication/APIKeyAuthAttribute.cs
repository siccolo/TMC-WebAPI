using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Authentication
{
    /*
        All REST API requests should check for the presence of an API Key in the header of the request. The value of this key should be “9876” or the request should not be considered authorized. 
        */
    /*
        other way:
            see AddAPIKeyAuthentication -> services.AddAuthentication(options => ....)
    */
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class APIKeyAuthorizationAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKeyHeaderName = "API";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKeyHeaderName, out var givenValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var config = context.HttpContext.RequestServices.GetRequiredService<IOptions<Authentication.APIKeyConfig>>();
            var apiKey = config.Value.key;

            if (!apiKey.Equals(givenValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
