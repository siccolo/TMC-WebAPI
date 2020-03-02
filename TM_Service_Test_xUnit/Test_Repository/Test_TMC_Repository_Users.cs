using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;

namespace Test_Repository
{
    public partial class Test_TMC_Repository
    {
        [Fact]
        public async Task TestRepository_GetAllUsersAsync()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var repository = new Repository.UserRepository(_DBContext, null);
            var result = await repository.AllAsync().ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"TestRepository_GetAllUsersAsync timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success && result.ResultValue.Any());
            Assert.Equal(5, result.ResultValue.Count());
        }

        [Theory]
        [InlineData(2)]
        public async Task TestRepository_GetOneUserAsync(int id)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var repository = new Repository.UserRepository(_DBContext , null);
            var result = await repository.OneByIdAsync(id).ConfigureAwait(false);
            
            sw.Stop();
            _Output.WriteLine($"TestRepository_GetOneUserAsync timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success);
            Assert.Equal("f2", result.ResultValue.FirstName);
        }
    }
}
