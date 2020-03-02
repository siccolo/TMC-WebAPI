using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core;

using Microsoft.Extensions.Logging;

namespace Repository
{
    public sealed class ProductRepository : BaseRepository<Models.ServiceProduct>, IRepository<Models.ServiceProduct, Result.Result<Models.ServiceProduct>>
    {
        
        private readonly ILogger<ProductRepository> _Logger;

        public ProductRepository(DataStore.IDataAccess dbContext, ILogger<ProductRepository> logger):base(dbContext)
        {
            _Logger = logger;
        }        
    }
}
