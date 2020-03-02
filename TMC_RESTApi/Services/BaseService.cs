using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class BaseService<Tin> where Tin: Models.IEntity
    {
        protected readonly Repository.IRepository<Tin, Result.Result<Tin>> _Repository;

        public BaseService(Repository.IRepository<Tin, Result.Result<Tin>> repository)
        {
            _Repository = repository;
        }

        public virtual async Task<Result.ResultList<Tin>> SelectAsync()
        {
            try
            {
                return await _Repository.AllAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new Result.ResultList<Tin>(ex);
            }
        }

        public virtual async Task<Result.ResultList<Tin>> FindAsync(Func<Tin, bool> criteria)
        {
            try
            {
                return await _Repository.FindAsync(criteria).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new Result.ResultList<Tin>(ex);
            }
        }

        public virtual  async Task<Result.Result<Tin>> SelectOneAsync(object id)
        {
            try
            {
                return await _Repository.OneByIdAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new Result.Result<Tin>(ex);
            }
        }
    }
}
