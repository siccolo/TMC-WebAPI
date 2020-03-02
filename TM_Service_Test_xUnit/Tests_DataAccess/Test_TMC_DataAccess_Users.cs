using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;

namespace Tests
{
    public partial class Test_TMC_DataAccess
    {
        [Fact]
        public async Task Test_GetAllUsersAsync()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var result = await _DBContext.GetAllUsersAsync(null).ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"Test_GetAllUsers timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success && result.ResultValue.Any());
            Assert.Equal(5, result.ResultValue.Count());
        }

        [Theory]
        [InlineData(2)]
        public async Task Test_GetOneUserAsync(int id)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var result = await _DBContext.GetUserAsync(id).ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"Test_GetOneUser timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success);
            Assert.Equal("f2", result.ResultValue.FirstName);
        }

        [Theory]
        [InlineData(4, "f4", "l4", "Activated")]
        [InlineData(5, "f5", "l5", "Activated")]
        [InlineData(6, "f6", "l6", "Activated")]
        [InlineData(7, "f7", "l7-suspended", "Suspended")]
        [InlineData(8, "f8", "l7-disabled", "Disabled")]
        public async Task Test_AddUserAsync(int id, string fname, string lname, string  status)
        {

            var usersBefore = await _DBContext.GetAllUsersAsync(null).ConfigureAwait(false);
            var countBefore = usersBefore.ResultValue.Count();

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var user = new Models.User(id, fname, lname, "", "", Core.UserStatusEnum.FromString(status), System.DateTime.Now);

            var result = await _DBContext.AddUserAsync(user).ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"Test_AddUserAsync timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success);

            var usersAfter = await _DBContext.GetAllUsersAsync(null).ConfigureAwait(false);
            var countAfter = usersAfter.ResultValue.Count();
            Assert.Equal(countAfter, countBefore + 1);

            var added = usersAfter.ResultValue.Where(e => e.Id == id).FirstOrDefault();
            Assert.Equal($"{fname} {lname}", added.FullName);
        }

        [Theory]
        [InlineData(4, null, "l4", "Activated")]
        [InlineData(5, "f5", null, "Activated")]
        [InlineData(-66, "f6", "l6", "Unknown")]
        public async Task Test_AddBadUserAsync(int id, string fname, string lname, string status)
        {
            var user = new Models.User(id, fname, lname, "", "", Core.UserStatusEnum.FromString(status), System.DateTime.Now);
            var result = await _DBContext.AddUserAsync(user).ConfigureAwait(false);

            Assert.True(!result.Success && result.Exception.Message == Extensions.DataConstants.InvalidEntity);
        }
    }
}
