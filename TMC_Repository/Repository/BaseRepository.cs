using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Repository
{
    public class BaseRepository<T> where T:Models.IEntity
    {
        protected readonly DataStore.IDataAccess _DBContext;
        public BaseRepository(DataStore.IDataAccess dbContext)
        {
            _DBContext = dbContext ?? throw new System.ArgumentNullException("_DBContext");
        }

        #region get all data
        public async Task<Result.ResultList<T>> AllAsync()
        {
            if (typeof(T) == typeof(Models.ServiceProduct))
            {
                var result = await _DBContext.GetAllProductsAsync().ConfigureAwait(false);
                return result as Result.ResultList<T>;
            }
            if (typeof(T) == typeof(Models.User))
            {
                var result = await _DBContext.GetAllUsersAsync(null).ConfigureAwait(false);
                return result as Result.ResultList<T>;
            }
            if (typeof(T) == typeof(Models.UserSubscription))
            {
                var result = await _DBContext.GetAllUserSubscriptionsAsync(null).ConfigureAwait(false);
                return result as Result.ResultList<T>;
            }
            return new Result.ResultList<T>(new System.InvalidOperationException());
        }
        #endregion

        #region get one data
        public virtual async Task<Result.Result<T>> OneByIdAsync(object id)
        {
            if (typeof(T) == typeof(Models.ServiceProduct))
            {
                var result = await _DBContext.GetServiceProductAsync((int)id).ConfigureAwait(false);
                return result as Result.Result<T>;
            }
            if (typeof(T) == typeof(Models.UserSubscription))
            {
                var result = await _DBContext.GetUserSubscriptionAsync((string)id).ConfigureAwait(false);
                return result as Result.Result<T>;
            }
            return new Result.Result<T>(new System.InvalidOperationException());
        }
        #endregion

        #region default implementations 
        public virtual  async Task<Result.ResultList<T>> FindAsync(Func<T, bool> criteria)
        {
            var result = await Task.Run(() => new Result.ResultList<T>(new System.NotImplementedException("FindAsync"))).ConfigureAwait(false);
            return result;
        }

        public virtual  async Task<Result.Result<Boolean>> AddAsync(T entity)
        {
            var result = await Task.Run(() => new Result.Result<Boolean>(new System.NotImplementedException("AddAsync"))).ConfigureAwait(false);
            return result;
        }

        public virtual async Task<Result.Result<Boolean>> DeleteAsync(object id)
        {
            var result = await Task.Run(() => new Result.Result<Boolean>(new System.NotImplementedException("DeleteAsync"))).ConfigureAwait(false);
            return result;
        }


        public virtual async Task<Result.Result<Boolean>> UpdateAsync(T entity)
        {
            var result = await Task.Run(() => new Result.Result<Boolean>(new System.NotImplementedException("UpdateAsync"))).ConfigureAwait(false);
            return result;
        }

        public virtual async Task<Result.Result<Boolean>> UpdateStatusAsync(object id, string value)
        {
            var result = await Task.Run(() => new Result.Result<Boolean>(new System.NotImplementedException("UpdateStatusAsync"))).ConfigureAwait(false);
            return result;
        }
        #endregion
    }
}
