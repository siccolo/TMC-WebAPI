using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public sealed class UserSubscription: BaseEntity, IEntity
    {
        /*
        public User User { get; private set; }
        public ServiceProduct ServiceProduct  { get; private set; }
        */
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ServiceProductId { get; set; }
        public override string Key => $"{UserId.ToString()}-{ServiceProductId.ToString()}";

        public override int Id => -1;

        public Core.SubscriptionEnum Status { get; set; }

        public UserSubscription(int userId, int productId)
        {
            UserId = userId;
            ServiceProductId = productId;
            Created = System.DateTime.Now;
            Status = Core.SubscriptionEnum.Activated;
        }
        
        public void SetStatus(Core.SubscriptionEnum status)
        {
            this.Status = status;
            SetAsModified();
        }
    }
}
