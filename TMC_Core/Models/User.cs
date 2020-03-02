using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public sealed class User: BaseEntity, IEntity
    {
        /*First Name, Last Name, Title, Suffix, FullName (automatic property), optional Date of Birth, SSN, Email, Password, and a way to determine if the user is active, disabled or suspended. */
        
        [Required] [StringLength(50,MinimumLength =2)]
        public string FirstName { get;   set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get;   set; }

        public string Suffix { get;   set; }

        public string Title { get;   set; }

        public System.DateTime? DOB{ get;   set; }
        public string SSN { get;   set; }
        public string Email { get;   set; }
        public string Password{ get;   set; }
        public string FullName => $"{FirstName} {LastName}";

        public Core.UserStatusEnum Status { get;   set; }

        public IEnumerable<ServiceProduct> Subscriptions { get; set; }

        public User() { }
        public User(int id, string firstName, string lastName, string suffix, string title, Core.UserStatusEnum status, System.DateTime created, System.DateTime? dob=null, string ssn="", string email="", string password = "")
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Suffix = Suffix;
            Title = title;
            Status = status;
            Created = created;
            DOB = dob;
            SSN = ssn;
            Email = email;
            Password = password;
        }

        public void SetStatus(Core.UserStatusEnum status)
        {
            this.Status = status;
            SetAsModified();
        }

        public void Disable()
        {
            this.Status = Core.UserStatusEnum.Disabled;
            SetAsModified();
        }
        public void Suspend()
        {
            this.Status = Core.UserStatusEnum.Suspended;
            SetAsModified();
        }
        public void Activate()
        {
            this.Status = Core.UserStatusEnum.Activated;
            SetAsModified();
        }

        public Result.Result<Boolean> UpdateDetails(User user)
        {
            //Update a User (User.Status cannot be updated via this endpoint) 
            FirstName = user.FirstName;
            LastName = user.LastName;
            Suffix = user.Suffix;
            Title = user.Title;
            DOB = user.DOB;
            SSN = user.SSN;
            Email = user.Email;
            Password = user.Password;
            SetAsModified();

            return new Result.Result<Boolean>(true);
        }

        public override bool IsValid
        {
            get
            {
                var context = new ValidationContext(this, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(this, context, results, true);
                return isValid;
            }
        }
    }
}
