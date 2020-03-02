using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        /*
       All REST API requests should check for the presence of an API Key in the header of the request. The value of this key should be “9876” or the request should not be considered authorized. 
       */
        /*
            other way:
                see AddAPIKeyAuthentication -> services.AddAuthentication(options => ....)
        */

        /*
        public static IServiceCollection AddAPIKeyAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var s = services.AddAuthentication(options =>
                                                {
                                                    options.DefaultAuthenticateScheme = ....
                                                    options.DefaultChallengeScheme = ....
                                                })
                .AddApiKeySupport(options => { });
            return s;
        }
        */

        public static IServiceCollection AddAPIKeyAuthentication(this IServiceCollection services, IConfiguration configuration, string configurationSectionName)
        {
            var s = services.Configure<Authentication.APIKeyConfig>(configuration.GetSection(configurationSectionName));
            var sp = services.BuildServiceProvider();
            var config = sp.GetService<IOptions<Authentication.APIKeyConfig>>();

            return s;
        }
    }
}
