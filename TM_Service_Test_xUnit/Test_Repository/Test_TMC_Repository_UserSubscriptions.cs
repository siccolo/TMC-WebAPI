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
        public async Task TestRepository_GetAllUserSubscriptionsAsync()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var repository = new Repository.SubscriptionRepository(_DBContext, null);
            var result = await repository.AllAsync().ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"TestRepository_GetAllUserSubscriptionsAsync timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success && result.ResultValue.Any());
            Assert.Single(result.ResultValue);
        }

        [Theory]
        [InlineData("1-2")]
        public async Task TestRepository_GetOneUserSubscriptionsAsync(string key)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var repository = new Repository.SubscriptionRepository(_DBContext, null);
            var result = await repository.OneByIdAsync(key).ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"TestRepository_GetOneUserSubscriptionsAsync timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success);
        }

        [Theory]
        [InlineData(1)]
        public async Task TestRepository_GetActiveUserSubscriptionsAsync(int userId)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var repository = new Repository.SubscriptionRepository(_DBContext, null);
            Func<Models.UserSubscription, bool> criteria = s => s.UserId == userId && s.Status.Value == Core.SubscriptionEnum.Activated.Value;
            var result = await repository.FindAsync(criteria).ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"TestRepository_GetActiveUserSubscriptionsAsync timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success && result.ResultValue.Any());
            Assert.Single(result.ResultValue);
        }


        [Theory]
        [InlineData(1,1)]
        public async Task TestRepository_CreateUserSubscriptionsAsync(int userId, int productId)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var repository = new Repository.SubscriptionRepository(_DBContext, null);
            var userSubscription = new Models.UserSubscription(userId, productId);
            var result = await repository.AddAsync(userSubscription).ConfigureAwait(false);

            sw.Stop();
            _Output.WriteLine($"TestRepository_GetActiveUserSubscriptionsAsync timing (code):" + sw.ElapsedMilliseconds.ToString());

            Assert.True(result.Success && result.ResultValue );
        }
    }
}
