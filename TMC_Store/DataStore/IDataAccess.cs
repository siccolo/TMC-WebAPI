using System;
using Models;
using Result;
using System.Threading.Tasks;

namespace DataStore
{
    public interface IDataAccess
    {
        void InitStorage();
        Task InitStorageAsync();

        #region user related
        Task<ResultList<Models.User>> GetAllUsersAsync(Func<Models.User, bool> criteria);
        Task<Result<Models.User>> GetUserAsync(int id); 
        Task<Result<Boolean>> AddUserAsync(Models.User data);
        Task<Result<Boolean>> UpdateUserAsync(Models.User data); 
        Task<Result<Boolean>> DeleteUserAsync(int id);
        #endregion

        #region product-service related
        Task<ResultList<Models.ServiceProduct>> GetAllProductsAsync();
        Task<Result<Models.ServiceProduct>> GetServiceProductAsync(int id);
        #endregion

        #region subscription related
        Task<ResultList<Models.UserSubscription>> GetAllUserSubscriptionsAsync(Func<Models.UserSubscription, bool> criteria);
        Task<Result.Result<Models.UserSubscription>> GetUserSubscriptionAsync(string key);
        Task<Result<Boolean>> AddUserSubscriptionAsync(Models.UserSubscription data);
        Task<Result<Boolean>> UpdateUserSubscriptionStatusAsync(string key, Core.SubscriptionEnum status);
        Task<Result<Boolean>> DeleteUserSubscriptionAsync(string key);
        #endregion
    }
}