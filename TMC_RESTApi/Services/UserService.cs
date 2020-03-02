using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Services
{
    public sealed class UserService : BaseService<Models.User>, IUserService
    {
        
        private readonly ILogger<UserService> _Logger;
        public UserService(Repository.IRepository<Models.User, Result.Result<Models.User>> repository, ILogger<UserService> logger):base(repository)
        {
            _Logger = logger;
        }

        
        public async Task<Result.Result<Boolean>> AddOneAsync(Models.User user)
        {
            try
            {
                return await _Repository.AddAsync(user).ConfigureAwait(false);
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

        public async Task<Result.Result<Boolean>> UpdateOneAsync(Models.User user)
        {
            try
            {
                //Update a User (User.Status cannot be updated via this endpoint) - get current from storage
                var resultGet = await _Repository.OneByIdAsync(user.Id).ConfigureAwait(false);
                if (!resultGet.Success)
                {
                    return new Result.Result<Boolean>(resultGet.Exception);
                }
                //Update a User (User.Status cannot be updated via this endpoint) - get current from storage
                user.Status = resultGet.ResultValue.Status;
                //save entry
                return await _Repository.UpdateAsync(user).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new Result.Result<Boolean>(ex);
            }
        }

        public async Task<Result.Result<Boolean>> UpdateOneStatusAsync(int id, string status)
        {
            try
            {
                return await _Repository.UpdateStatusAsync(id, status).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new Result.Result<Boolean>(ex);
            }
        }
    }
}
