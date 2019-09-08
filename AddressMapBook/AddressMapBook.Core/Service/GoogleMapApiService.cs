using AddressMapBook.Core.Helper;
using AddressMapBook.Core.Interface;
using AddressMapBook.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AddressMapBook.Core.Service
{
    //AddressMapBook_MapsAPI
    public class GoogleMapApiService : IGoogleMapsApiService
    {
        private readonly IApiService _apiService;

        public GoogleMapApiService(IApiService apiService)
        {
            _apiService = apiService;
            _apiService.BaseUri = ApiBaseAddress;
        }

        static string _googleMapsKey;
        static string _googlePlaceKey;

        private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
        private const string GeoBaseAddress = "https://www.googleapis.com/";

        public static void Initialize()
        {
            _googlePlaceKey = Util.PlaceApiKey;
            _googleMapsKey = Util.MapApiKey;
        }

        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public async Task<GooglePlaceAutoCompleteResult> GetPlaces(string text)
        {

            GooglePlaceAutoCompleteResult results = null;
            _apiService.BaseUri = ApiBaseAddress;
            var res = await _apiService.GetResponseAsync<GooglePlaceAutoCompleteResult>($"api/place/autocomplete/json?input={Uri.EscapeUriString(text)}&key={_googlePlaceKey}");
            results = res.Result;

            return results;

        }

        public async Task<PlaceResult> GetPlaceDetails(string placeId)
        {
            PlaceResult result = null;
            _apiService.BaseUri = ApiBaseAddress;
            var res = await _apiService.GetResponseAsync<PlaceResult>($"api/place/details/json?placeid={Uri.EscapeUriString(placeId)}&key={_googlePlaceKey}");
            result = res.Result;

            return result;
        }

        public async Task<Geometry> GetCurrentLocation()
        {
            Geometry result = null;
            _apiService.BaseUri = GeoBaseAddress;
            var res = await _apiService.PostResponse<Geometry>($"geolocation/v1/geolocate?key={_googlePlaceKey}");
            result = res.Result;

            return result;
        }

        //public async Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude)
        //{
        //    GoogleDirection googleDirection = new GoogleDirection();

        //    var res = await _apiService.GetResponseAsync<GoogleDirection>($"api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={originLatitude},{originLongitude}&destination={destinationLatitude},{destinationLongitude}&key={_googlePlaceKey}");
        //    googleDirection = res.Result;

        //    return googleDirection;
        //}
    }
}
