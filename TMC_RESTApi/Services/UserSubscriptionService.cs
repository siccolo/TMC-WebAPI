using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class UserSubscriptionService : BaseService<Models.UserSubscription>, IUserSubscriptionService
    {

        private readonly ILogger<UserSubscriptionService> _Logger;
        public UserSubscriptionService(Repository.IRepository<Models.UserSubscription, Result.Result<Models.UserSubscription>> repository, ILogger<UserSubscriptionService> logger) : base(repository)
        {
            _Logger = logger;
        }
                 
        public async Task<Result.Result<Boolean>> AddOneAsync(Models.UserSubscription subscription)
        {
            try
            {
                return await _Repository.AddAsync(subscription).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new Result.Result<Boolean>(ex);
            }
        }

        public async Task<Result.Result<Boolean>> DeleteOneAsync(object id)
        {
            try
            {
                return await _Repository.DeleteAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new Result.Result<Boolean>(ex);
            }
        }

    }
}