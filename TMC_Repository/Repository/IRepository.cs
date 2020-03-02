using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Repository
{

    public interface IRepository<Tin, Tout>
        where Tin:Models.IEntity
        where Tout : Result.IResult<Tin>

    {
        Task<Result.ResultList<Tin>> AllAsync();
        
        Task<Result.ResultList<Tin>> FindAsync(Func<Tin, bool> criteria);

        Task<Tout> OneByIdAsync(object id);
        Task<Result.Result<Boolean>> AddAsync(Tin entity);
        Task<Result.Result<Boolean>> UpdateAsync(Tin entity);

        Task<Result.Result<Boolean>> UpdateStatusAsync(object id, string status);

        Task<Result.Result<Boolean>> DeleteAsync(object id);
    }
     
}
