using AddressMapBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddressMapBook.Core.Interface
{
    public interface IApiService
    {
        string BaseUri { get; set; }

        Task<GenericApiResponse<T>> GetResponseAsync<T>(string uriRequest);
        //Task<BoolResponse> PostReponse<Param>(string uriRequest, Param param);
        Task<GenericApiResponse<T>> PostResponse<T>(string uriRequest);
    }
}
