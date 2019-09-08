using System;
using System.Collections.Generic;
using System.Text;

namespace AddressMapBook.Core.Models
{
    class Exceptions
    {
    }

    public class NoInternetConnectionException : Exception
    {
        public string ExceptionMessage { get; set; }
    }
}
