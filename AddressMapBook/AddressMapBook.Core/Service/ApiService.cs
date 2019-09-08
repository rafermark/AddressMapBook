using AddressMapBook.Core.Interface;
using AddressMapBook.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddressMapBook.Core.Service
{
    public class ApiService : IApiService
    {
        private readonly IConnection _connection;


        private readonly HttpClient _client;
        private readonly HttpClientHandler _clientHandler;
        private readonly CookieContainer _cookieContainer;


        public ApiService(IConnection connection)
        {
            _connection = connection;

            _cookieContainer = new CookieContainer();
            _clientHandler = new HttpClientHandler() { CookieContainer = _cookieContainer };

            _client = new HttpClient(_clientHandler);
            _client.DefaultRequestHeaders.Add("x-requested-with", "XMLHttpRequest");

        }

        public string BaseUri
        {
            get { return _client.BaseAddress.AbsoluteUri; }
            set
            {
                _client.BaseAddress = new Uri(value);
            }
        }

        public async Task<GenericApiResponse<T>> GetResponseAsync<T>(string uriRequest)
        {

            var response = new GenericApiResponse<T>();

            //cancellationToken.ThrowIfCancellationRequested();

            _connection.CheckConnectivity();

            try
            {
                HttpResponseMessage httpResponse = await _client.GetAsync(uriRequest);

                response.IsSuccess = httpResponse.IsSuccessStatusCode;
                response.Message = httpResponse.ReasonPhrase;
                response.StatusCode = (int)httpResponse.StatusCode;

                if (response.IsSuccess)
                {
                    //cancellationToken.ThrowIfCancellationRequested();

                    string result = await httpResponse.Content.ReadAsStringAsync();
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    response.Result = JsonConvert.DeserializeObject<T>(result, settings);

                }

            }
            catch (OperationCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = -1;
                response.Message = GetExceptionMessage(ex);
            }

            //cancellationToken.ThrowIfCancellationRequested();

            return response;
        }

        public async Task<GenericApiResponse<T>> PostResponse<T>(string uriRequest)
        {
            var response = new GenericApiResponse<T>();

            //cancellationToken.ThrowIfCancellationRequested();

            _connection.CheckConnectivity();

            try
            {
                HttpResponseMessage httpResponse = await _client.PostAsync(uriRequest, null);

                response.IsSuccess = httpResponse.IsSuccessStatusCode;
                response.Message = httpResponse.ReasonPhrase;
                response.StatusCode = (int)httpResponse.StatusCode;

                if (response.IsSuccess)
                {
                    //cancellationToken.ThrowIfCancellationRequested();

                    string result = await httpResponse.Content.ReadAsStringAsync();
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    response.Result = JsonConvert.DeserializeObject<T>(result, settings);

                }

            }
            catch (OperationCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = -1;
                response.Message = GetExceptionMessage(ex);
            }

            //cancellationToken.ThrowIfCancellationRequested();

            return response;
        }

        //public async Task<BoolResponse> PostReponse<Param>(string uriRequest, Param param)
        //{
        //    _connection.CheckConnectivity();
        //    var response = new BoolResponse();
        //    try
        //    {
        //        //cancellationToken.ThrowIfCancellationRequested();

        //        string json = JsonConvert.SerializeObject(param);
        //        var content = new StringContent(json, Encoding.UTF8, "text/json");
        //        HttpResponseMessage httpResponse = await _client.PostAsync(uriRequest, content);

        //        response.IsSuccess = httpResponse.IsSuccessStatusCode;
        //        response.Message = httpResponse.ReasonPhrase;
        //        response.StatusCode = (int)httpResponse.StatusCode;
        //    }
        //    catch (OperationCanceledException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.Message = GetExceptionMessage(ex);
        //    }

        //    //cancellationToken.ThrowIfCancellationRequested();

        //    return response;
        //}





        string GetExceptionMessage(Exception ex)
        {

            var exceptionMessage = ex.Message;


            Exception realerror = ex;
            while (realerror.InnerException != null)
            {
                exceptionMessage += $"*{realerror.Message}* ";
                realerror = realerror.InnerException;
            }

            return exceptionMessage;
        }
    }
}
