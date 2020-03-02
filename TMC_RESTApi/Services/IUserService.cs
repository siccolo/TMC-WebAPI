using Models;
using Result;
using System;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService:IReadWriteService<Models.User>
    {
        Task<Result.Result<bool>> UpdateOneAsync(Models.User entity);
        Task<Result<bool>> UpdateOneStatusAsync(int id, string status);
    }
}