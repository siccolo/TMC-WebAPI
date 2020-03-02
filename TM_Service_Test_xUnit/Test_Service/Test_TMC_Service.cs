using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;

namespace Test_Service
{
    public partial class Test_TMC_Service
    {
        private readonly ITestOutputHelper _Output;
        private DataStore.DataAccess _DBContext;
        public Test_TMC_Service(ITestOutputHelper output)
        {
            _Output = output;
            var dbconfig = Helpers.AppSettingsExtensions.GetApplicationConfiguration(AppContext.BaseDirectory);
            _DBContext = new DataStore.DataAccess(dbconfig);
            _DBContext.InitStorage();
        }

        [Fact]
        public async Task TestService_SelectAsync()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var repository = new Repository.UserRepository(_DBContext, null);
            var service = new Services.UserService(repository, null);
            var result = await service.SelectAsync().ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"TestService_SelectAsync timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success && result.ResultValue.Any());
            Assert.Equal(5, result.ResultValue.Count());
        }
    }
}
