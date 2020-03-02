using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Services
{
    public sealed class ProductService:BaseService<Models.ServiceProduct>, IProductService
    {
        private readonly ILogger<UserService> _Logger;
        public ProductService(Repository.IRepository<Models.ServiceProduct, Result.Result<Models.ServiceProduct>> repository, ILogger<UserService> logger):base(repository)
        {
            _Logger = logger;
        }
    }
}
