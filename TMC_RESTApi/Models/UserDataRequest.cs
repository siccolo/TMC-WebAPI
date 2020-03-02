using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserDataRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; } //integer unique identifier

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string Title { get; set; }

        public System.DateTime? DOB { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Models.User ToModel()
        {
            Models.User user = new Models.User()
            {
                 Id = this.Id
                 , FirstName = this.FirstName
                 , LastName= this.LastName
                 , Suffix = this.Suffix
                 , Title = this.Title
                 , DOB = this.DOB
                 , SSN = this.SSN
                 , Email = this.Email
                 , Password = this.Password
            };
            return user;
        }
    }
    public sealed class UserDataCreateRequest:UserDataRequest
    {
        public string Status { get; set; }

        public UserDataCreateRequest() { }  

        public override Models.User ToModel()
        {
            Models.User user = new Models.User()
            {
                 Id = this.Id
                 , FirstName = this.FirstName
                 , LastName= this.LastName
                 , Suffix = this.Suffix
                 , Title = this.Title
                 , DOB = this.DOB
                 , SSN = this.SSN
                 , Email = this.Email
                 , Password = this.Password
                 , Status = Core.UserStatusEnum.FromString(this.Status)
            };
            return user;
        }
    }

    public sealed class UserDataUpdateRequest : UserDataRequest
    {
        public UserDataUpdateRequest() { }
    }
}
