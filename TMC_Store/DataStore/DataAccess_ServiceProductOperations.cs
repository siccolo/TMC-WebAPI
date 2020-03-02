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
       
        public async Task<Result.ResultList<Models.ServiceProduct>> GetAllProductsAsync()
        {
            return await Products.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<Result.Result<Models.ServiceProduct>> GetServiceProductAsync(int id)
        {
            return await Products.GetOneAsync(id).ConfigureAwait(false);
        }
    }
}
