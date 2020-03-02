using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IReadService<T> where T:Models.IEntity
    {
        Task<Result.ResultList<T>> SelectAsync( ); 
        Task<Result.ResultList<T>> FindAsync(Func<T, bool> criteria);
        Task<Result.Result<T>> SelectOneAsync(object id);
    }

    public interface IReadWriteService<T>:IReadService<T> where T : Models.IEntity 
    {
        Task<Result.Result<bool>> AddOneAsync(T entity);
        Task<Result.Result<bool>> DeleteOneAsync(object id);
    }
}
