using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Extensions
{
    public static partial class DataStoreExtensions
    {

        #region add one record
        public static Result.Result<Boolean> AddOne<T>(this ConcurrentDictionary<string, T> dict, T entry)
        {
            if (!((Models.IEntity)entry).IsValid)
            {
                return new Result.Result<Boolean>(new SystemException(Extensions.DataConstants.InvalidEntity));
            }

            string key = ((Models.IEntity)entry).Key;
            if (!dict.ContainsKey(key))
            {
                ((Models.IEntity)entry).SetCreatedDateTime();
                bool added = dict.TryAdd(key, entry);
                return new Result.Result<Boolean>(added);
            }
            else
            {
                return new Result.Result<Boolean>(new SystemException(Extensions.DataConstants.FailedToSave));
            }
        }

        public static async Task<Result.Result<Boolean>> AddOneAsync<T>(this ConcurrentDictionary<string, T> dict, T entry)
        {
            var result = await Task.Run(() => dict.AddOne(entry)).ConfigureAwait(false);
            return result;
        }
        #endregion

        #region remove one record
        public static Result.Result<Boolean> RemoveOne<T>(this ConcurrentDictionary<string, T> dict, string key)
        {
            if (dict.ContainsKey(key))   //or //if (Users.Any(e => e.Value.Id == id))
            {
                var removed = dict.TryRemove(key, out T removedData);
                return new Result.Result<Boolean>(removed);
            }
            else
            {
                return new Result.Result<Boolean>(new SystemException(Extensions.DataConstants.FailedToFind));
            }
        }
        public static Result.Result<Boolean> RemoveOne<T>(this ConcurrentDictionary<string, T> dict, int id)
        {
            string key = id.ToString();
            return dict.RemoveOne(key);
        }       

        public static async Task<Result.Result<Boolean>> RemoveOneAsync<T>(this ConcurrentDictionary<string, T> dict, int id)
        {
            var result = await Task.Run(() => dict.RemoveOne(id)).ConfigureAwait(false);
            return result;
        }
        public static async Task<Result.Result<Boolean>> RemoveOneAsync<T>(this ConcurrentDictionary<string, T> dict, string key)
        {
            var result = await Task.Run(() => dict.RemoveOne(key)).ConfigureAwait(false);
            return result;
        }
        #endregion


    }
}
