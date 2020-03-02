using System;
using System.Collections.Generic;
using System.Text;

namespace Result
{
    public interface IResult<T>
    {
        bool Success { get; }
        T ResultValue { get; }

        System.Exception Exception { get; }

        string AdditionalInfo { get; }
    }
}
