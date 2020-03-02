using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Options;

namespace DataStore
{
    public sealed partial class DataAccess : IDataAccess
    {
        private readonly DataStore.DataConfig _DataConfig;
        public DataAccess(IOptions<DataStore.DataConfig> options)
        {
            _DataConfig = options.Value ?? throw new System.ArgumentNullException("DataConfig");
        }
        public DataAccess(DataStore.DataConfig config)
        {
            _DataConfig = config ?? throw new System.ArgumentNullException("DataConfig");
        }
    }
}
