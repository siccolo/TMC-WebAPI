using System;
using System.Collections.Generic;
using System.Text;

namespace Result
{
    public sealed class ResultList<T>:IResult<IEnumerable<T>>
    {
        public bool Success { get; private set; }
        public IEnumerable<T> ResultValue { get; private set; }

        public string AdditionalInfo { get; private set; } = "";

        public System.Exception Exception { get; private set; }

        public ResultList(System.Exception exception, string info = "")
        {
            Success = false;
            Exception = new System.Exception(exception.Message);
            AdditionalInfo = info;
        }

        public ResultList(IEnumerable<T> result, string info = "")
        {
            Success = true;
            ResultValue = result;
            AdditionalInfo = info;
        }
    }
}
