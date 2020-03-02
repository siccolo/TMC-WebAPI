using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace DataStore
{
    public sealed partial class DataAccess 
    {

        #region get all user subscriptions or with search filter
        public async Task<Result.ResultList<Models.UserSubscription>> GetAllUserSubscriptionsAsync(Func<Models.UserSubscription, bool> criteria)
        {
            return await UserSubscriptions.GetAllAsync(criteria).ConfigureAwait(false);
        }
        #endregion

        #region get one user subscription by key
        public async Task<Result.Result<Models.UserSubscription>> GetUserSubscriptionAsync(string key)
        {
            return await UserSubscriptions.GetOneAsync(key).ConfigureAwait(false);
        }
        #endregion

        #region add new user subscription
        public async Task<Result.Result<Boolean>> AddUserSubscriptionAsync(Models.UserSubscription entity)
        {
            return await UserSubscriptions.AddOneAsync(entity).ConfigureAwait(false); 
        }
        #endregion

        #region update user subscription status
        private Result.Result<Boolean> UpdateUserSubscriptionStatus(string key, Core.SubscriptionEnum status)
        {
            if (!UserSubscriptions.ContainsKey(key))
            {
                return new Result.Result<Boolean>(new SystemException(Extensions.DataConstants.FailedToFind));
            }
            UserSubscriptions[key].SetStatus(status);
            return new Result.Result<Boolean>(true);
        }

        public async Task<Result.Result<Boolean>> UpdateUserSubscriptionStatusAsync(string key, Core.SubscriptionEnum status)
        {
            var result = await Task.Run(() => UpdateUserSubscriptionStatus(key, status)).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region delete user subscription 
        public async Task<Result.Result<Boolean>> DeleteUserSubscriptionAsync(string key)
        {
           return await UserSubscriptions.RemoveOneAsync(key).ConfigureAwait(false);
        }
        #endregion
    }
}
