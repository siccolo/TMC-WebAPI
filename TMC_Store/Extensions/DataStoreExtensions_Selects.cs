using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Extensions
{
    public static partial class DataStoreExtensions
    {
        #region sync operations
        public static Result.ResultList<T> GetAll<T>(this ConcurrentDictionary<string, T> dict) 
        {
            var list = dict.Select(e => e.Value);
            var results = new Result.ResultList<T>(list);
            return results;
        }

        public static Result.ResultList<T> GetAll<T>(this ConcurrentDictionary<string, T> dict, Func<T, bool> criteria) 
        {
            var list = dict.Select(e => e.Value);
            if (criteria != null)
            {
                list = list.Where(criteria);
            }
            var results = new Result.ResultList<T>(list);
            return results;
        }

        public static Result.Result<T> GetOne<T>(this ConcurrentDictionary<string, T> dict, int id) 
        {
            if (dict.ContainsKey(id.ToString()))   //or //if (Users.Any(e => e.Value.Id == id))
            {
                var data = dict.Where(e => ((Models.IEntity)e.Value).Id == id).FirstOrDefault().Value;
                return new Result.Result<T>(data);
            }
            return new Result.Result<T>(new System.Exception(Extensions.DataConstants.FailedToFind));
        }

        public static Result.Result<T> GetOne<T>(this ConcurrentDictionary<string, T> dict, string key) 
        {
            if (dict.ContainsKey(key))  
            {
                var data = dict.Where(e => ((Models.IEntity)e.Value).Key == key).FirstOrDefault().Value;
                return new Result.Result<T>(data);
            }
            return new Result.Result<T>(new System.Exception(Extensions.DataConstants.FailedToFind));
        }
        #endregion

        #region async operations
        public static async Task<Result.ResultList<T>> GetAllAsync<T>(this ConcurrentDictionary<string, T> dict)
        {
            var entries = await Task.Run(() => dict.GetAll()).ConfigureAwait(false);
            return entries;
        }

        public static async Task<Result.ResultList<T>> GetAllAsync<T>(this ConcurrentDictionary<string, T> dict, Func<T, bool> criteria)
        {
            var entries = await Task.Run(() => dict.GetAll(criteria)).ConfigureAwait(false);
            return entries;
        }


        public static async Task<Result.Result<T>> GetOneAsync<T>(this ConcurrentDictionary<string, T> dict, int id)
        {
            var entry = await Task.Run(() => dict.GetOne(id)).ConfigureAwait(false);
            return entry;
        }
        public static async Task<Result.Result<T>> GetOneAsync<T>(this ConcurrentDictionary<string, T> dict, string key)
        {
            var entry = await Task.Run(() => dict.GetOne(key)).ConfigureAwait(false);
            return entry;
        }
        #endregion
    }
}
