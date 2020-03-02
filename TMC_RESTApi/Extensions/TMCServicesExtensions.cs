using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTMCServices(this IServiceCollection services, IConfiguration configuration, string configurationSectionName)
        {
            var s = services
                    .Configure<DataStore.DataConfig>(configuration.GetSection(configurationSectionName))
                    .AddSingleton<DataStore.IDataAccess, DataStore.DataAccess>()

                    //          --- repositories ---
                    //  users
                    .AddTransient(typeof(Repository.IRepository<Models.User, Result.Result<Models.User>>), typeof(Repository.UserRepository))
                    //  services - aka user products
                    .AddTransient(typeof(Repository.IRepository<Models.ServiceProduct, Result.Result<Models.ServiceProduct>>), typeof(Repository.ProductRepository))
                    //  subscriptions
                    .AddTransient(typeof(Repository.IRepository<Models.UserSubscription, Result.Result<Models.UserSubscription>>), typeof(Repository.SubscriptionRepository))

                    //          --- repository services ---
                    //  user service
                    .AddTransient<Services.IUserService, Services.UserService>()
                    //  product service
                    .AddTransient<Services.IProductService, Services.ProductService>()
                    //  subscription service
                    .AddTransient<Services.IUserSubscriptionService, Services.UserSubscriptionService>()
                    ;
            return s;
        }
    }

    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PopulateTMCStorage(this IApplicationBuilder app)
        {
            Task.Run(() => { 
                app.ApplicationServices.GetRequiredService<DataStore.IDataAccess>().InitStorage(); 
            });

            return app;
        }
    }
}
