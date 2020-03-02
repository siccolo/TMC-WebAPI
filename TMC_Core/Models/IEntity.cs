using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface IEntity
    {
        int Id { get; }
        string Key { get; }
        System.DateTime Created { get;  }
        System.DateTime Modfied { get;  }
        bool IsValid { get; }

        void SetCreatedDateTime();
    }
}
