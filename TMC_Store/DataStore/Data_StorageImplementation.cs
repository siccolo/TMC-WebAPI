using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataStore
{
    public sealed partial class DataAccess
    {
        #region dummy data for now
        public async Task InitStorageAsync()
        {
            await Task.Run(() => InitStorage()).ConfigureAwait(false);
            return;
        }
        private ConcurrentDictionary<string, Models.User> Users = new ConcurrentDictionary<string, Models.User>();
        private ConcurrentDictionary<string, Models.ServiceProduct> Products = new ConcurrentDictionary<string, Models.ServiceProduct>();
        private ConcurrentDictionary<string, Models.UserSubscription> UserSubscriptions = new ConcurrentDictionary<string, Models.UserSubscription>();
        public void InitStorage()
        {
            InitStorageUsers();
            InitStorageProducts();
            InitStorageUserSubscription();
        }
        private void InitStorageUsers()
        {
            var today = System.DateTime.Now;
            Users.TryAdd("1", new Models.User(1, "f1", "l1-activated", "", "", Core.UserStatusEnum.Activated, today));
            Users.TryAdd("11", new Models.User(11, "f11", "l11-activated", "", "", Core.UserStatusEnum.Activated, today));

            Users.TryAdd("2", new Models.User(2, "f2", "l2-disabled", "", "", Core.UserStatusEnum.Disabled, today));

            Users.TryAdd("3", new Models.User(3, "f3", "l3-suspended", "", "", Core.UserStatusEnum.Suspended, today));
            Users.TryAdd("33", new Models.User(33, "f33", "l33-suspended", "", "", Core.UserStatusEnum.Suspended, today));
        }
        private void InitStorageProducts()
        {
            var today = System.DateTime.Now;
            Products.TryAdd("1", new Models.ServiceProduct(1, "product1", 10.00, today));
            Products.TryAdd("2", new Models.ServiceProduct(2, "product2", 20.00, today));
        }
        private void InitStorageUserSubscription()
        {
            //subscription -> user #1, and product #2
            UserSubscriptions.TryAdd("1-2", new Models.UserSubscription(1, 2));
        }
        #endregion
    }
}
