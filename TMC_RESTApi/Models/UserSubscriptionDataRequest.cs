using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserSubscriptionDataRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; } 

		[Required]
        [Range(1, int.MaxValue)]
        public int ServiceProductId { get; set; }

        public Models.UserSubscription ToModel()
        {
            Models.UserSubscription userSubscription = new Models.UserSubscription(this.UserId, this.ServiceProductId);
            return userSubscription;
        }
    }
}
