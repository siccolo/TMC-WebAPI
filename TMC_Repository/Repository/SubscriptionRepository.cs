using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core;

using Microsoft.Extensions.Logging;

namespace Repository
{

    public sealed class SubscriptionRepository :BaseRepository<Models.UserSubscription>, IRepository<Models.UserSubscription, Result.Result<Models.UserSubscription>>
    {

        private readonly ILogger<SubscriptionRepository> _Logger;

        public SubscriptionRepository(DataStore.IDataAccess dbContext, ILogger<SubscriptionRepository> logger):base(dbContext)
        {
            _Logger = logger;
        }

        public override async Task<Result.ResultList<Models.UserSubscription>> FindAsync(Func<Models.UserSubscription, bool> criteria)
        {
            var result = await _DBContext.GetAllUserSubscriptionsAsync(criteria).ConfigureAwait(false);
            return result;
        }

        public override async Task<Result.Result<Boolean>> AddAsync(Models.UserSubscription entity)
        {
            var result = await _DBContext.AddUserSubscriptionAsync(entity).ConfigureAwait(false);
            return result;
        }

        public override  async Task<Result.Result<Boolean>> DeleteAsync(object id)
        {
            var result = await _DBContext.DeleteUserSubscriptionAsync((string)id).ConfigureAwait(false);
            return result;
        }
         
        public override  async Task<Result.Result<Boolean>> UpdateStatusAsync(object id, string value)
        {
            Core.SubscriptionEnum status = Core.SubscriptionEnum.FromString(value);
            var result = await _DBContext.UpdateUserSubscriptionStatusAsync((string)id, status).ConfigureAwait(false);
            return result;
        }
    }
}
