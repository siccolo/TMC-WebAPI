using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BaseEntity
    {

        [Required]
        [Range(1, int.MaxValue)]
        public virtual int Id { get;   set; } 
        public virtual string Key => Id.ToString();

        public System.DateTime Created { get;   set; }
        public System.DateTime Modfied { get;   set; }

        public virtual bool IsValid  => true;

        protected void SetAsModified()
        {
            this.Modfied = System.DateTime.Now;
        }

        public void SetCreatedDateTime()
        {
            this.Created = System.DateTime.Now;
        }
    }
}
