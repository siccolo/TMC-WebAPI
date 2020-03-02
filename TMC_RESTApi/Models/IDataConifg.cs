using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface IDataConifg
    {
        string Server { get; set; }
        string Database { get; set; }
        string UserId { get; set; }
        string Password { get; set; }
    }
}
