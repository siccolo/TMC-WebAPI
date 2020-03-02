using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public sealed class ServiceProduct:BaseEntity, IEntity
    {
        /*
            The Service entity should have at least an Id, Name, and Price. This collection is arbitrary and can be hard coded. 
            Each user and service should have an integer unique identifier. The entity should also track the date the record was created and last modified. 
        */       

        [StringLength(50, MinimumLength = 10)]
        public string Name { get; private set; }
        
        [Required]
        [Range(1, 5000)]
        public double Price { get; private set; }
        
        public ServiceProduct(int id, string name, double price, System.DateTime created)
        {
            Id = id;
            Name = name;
            Price = price;
            base.Created = created;
        }

        public Result.Result<Boolean> UpdateDetails(ServiceProduct product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Created = product.Created;
            SetAsModified();
            return new Result.Result<Boolean>(true);
        }

        public override bool IsValid
        {
            get
            {
                var context = new ValidationContext(this, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(this, context, results);
                return isValid;
            }
        }
    }
}
