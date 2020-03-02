using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;

namespace Tests
{
    public partial class Test_TMC_DataAccess
    {
        private readonly ITestOutputHelper _Output;
        private DataStore.DataAccess _DBContext;
        public Test_TMC_DataAccess(ITestOutputHelper output)
        {
            _Output = output;
            var dbconfig = Helpers.AppSettingsExtensions.GetApplicationConfiguration(AppContext.BaseDirectory);
            _DBContext = new DataStore.DataAccess(dbconfig);
            _DBContext.InitStorage();
        }
    }
}
