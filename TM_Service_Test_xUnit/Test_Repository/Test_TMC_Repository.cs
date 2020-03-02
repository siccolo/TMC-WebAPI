using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;

namespace Test_Repository
{
    public partial class Test_TMC_Repository
    {
        private readonly ITestOutputHelper _Output;
        private DataStore.DataAccess _DBContext;
        public Test_TMC_Repository(ITestOutputHelper output)
        {
            _Output = output;
            var dbconfig = Helpers.AppSettingsExtensions.GetApplicationConfiguration(AppContext.BaseDirectory);
            _DBContext = new DataStore.DataAccess(dbconfig);
            _DBContext.InitStorage();
        }
    }
}
