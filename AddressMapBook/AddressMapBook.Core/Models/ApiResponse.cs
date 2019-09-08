using System;
using System.Collections.Generic;
using System.Text;

namespace AddressMapBook.Core.Models
{
    public class GenericApiResponse<T> : BaseResponse
    {
        public T Result { get; set; }
    }

    public class BoolResponse : BaseResponse
    {

    }

    public abstract class BaseResponse
    {
        public BaseResponse()
        {

        }


        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
