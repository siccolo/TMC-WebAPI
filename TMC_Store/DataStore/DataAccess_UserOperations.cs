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
        #region get all users
       
        public async Task<Result.ResultList<Models.User>> GetAllUsersAsync(Func<Models.User, bool> criteria)
        {
            return await Users.GetAllAsync<Models.User>(criteria).ConfigureAwait(false);
        }
        #endregion

        #region get one user
        public async Task<Result.Result<Models.User>> GetUserAsync(int id)
        {
            return await Users.GetOneAsync(id).ConfigureAwait(false);
        }
        #endregion

        #region add one user
        public async Task<Result.Result<Boolean>> AddUserAsync(Models.User data)
        {
            return  await Users.AddOneAsync(data).ConfigureAwait(false);
        }
        #endregion

        #region remove user
        public async Task<Result.Result<Boolean>> DeleteUserAsync(int id)
        {
            return await  Users.RemoveOneAsync(id).ConfigureAwait(false);
        }
        #endregion

        #region update user
        private Result.Result<Boolean> UpdateUser(Models.User data)
        {
            if (Users.ContainsKey(data.Key))
            {
                var storeddata = Users[data.Key] as Models.User;
                storeddata.UpdateDetails(data);
                Users[data.Key] = storeddata;
                return new Result.Result<Boolean>(true);
            }
            else
            {
                return new Result.Result<Boolean>( new SystemException(Extensions.DataConstants.FailedToFind));
            }
        }

        public async Task<Result.Result<Boolean>> UpdateUserAsync(Models.User data)
        {
            var result = await Task.Run(() => UpdateUser(data)).ConfigureAwait(false);
            return result;
        }
        #endregion
    }
}
