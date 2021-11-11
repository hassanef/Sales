using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Exceptions
{
    public class SalesException : Exception
    {
        public SalesException()
        { }

        public SalesException(string message)
            : base(message)
        { }

        public SalesException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

