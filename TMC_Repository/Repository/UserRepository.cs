using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core;

using Microsoft.Extensions.Logging;

namespace Repository
{
    public sealed class UserRepository : BaseRepository<Models.User>, IRepository<Models.User,Result.Result<Models.User>>
    {
        private readonly ILogger<UserRepository> _Logger;

        public UserRepository(DataStore.IDataAccess dbContext, ILogger<UserRepository> logger):base(dbContext)
        {
            _Logger = logger;
        }

        public override async Task<Result.Result<Models.User>> OneByIdAsync(object id)
        {
            var resultUserGet = await _DBContext.GetUserAsync((int)id).ConfigureAwait(false);
            Func<Models.UserSubscription, bool> criteriaUserSubscriptions = us => us.UserId == (int)id;
            var resultUSerSubscriptionsGet = await _DBContext.GetAllUserSubscriptionsAsync(criteriaUserSubscriptions).ConfigureAwait(false);
            if(resultUSerSubscriptionsGet.Success && resultUSerSubscriptionsGet.ResultValue.Any())
            {
                //resultUSerSubscriptionsGet.ResultValue odls all ServiceProductId for a given user, 
                var resultProductsGet  = await _DBContext.GetAllProductsAsync().ConfigureAwait(false);
                var listWithFullInfo =
                    from pp in resultProductsGet.ResultValue
                    join us in resultUSerSubscriptionsGet.ResultValue on pp.Id equals us.ServiceProductId
                    select pp;
                resultUserGet.ResultValue.Subscriptions = listWithFullInfo;
            }
            else
            {
                resultUserGet.ResultValue.Subscriptions = new List<Models.ServiceProduct>();
            }
            return resultUserGet as Result.Result<Models.User>;
        }

        public override async Task<Result.ResultList<Models.User>> FindAsync(Func<Models.User, bool> criteria)
        {
            var result = await _DBContext.GetAllUsersAsync(criteria).ConfigureAwait(false);
            return result;
        }

        public override async Task<Result.Result<Boolean>> AddAsync(Models.User entity)
        {
            var result = await _DBContext.AddUserAsync(entity).ConfigureAwait(false);
            return result;
        }

        public override  async Task<Result.Result<Boolean>> DeleteAsync(object id)
        {
            var result = await _DBContext.DeleteUserAsync((int)id).ConfigureAwait(false);
            return result;
        }
                
        public override  async Task<Result.Result<Boolean>> UpdateAsync(Models.User entity)
        {
            var resultSet = await _DBContext.UpdateUserAsync(entity).ConfigureAwait(false);
            return resultSet;
        }

        public override async Task<Result.Result<Boolean>> UpdateStatusAsync(object id,   string value)
        {
            var resultGet = await _DBContext.GetUserAsync((int)id).ConfigureAwait(false);
            if (!resultGet.Success)
            {
                return  new Result.Result<Boolean>(resultGet.Exception);
            }
            var user = resultGet.ResultValue;
            user.SetStatus ( Core.UserStatusEnum.FromString(value));
            var resultSet = await _DBContext.UpdateUserAsync(user).ConfigureAwait(false);
            return resultSet;
        }
    }
}
