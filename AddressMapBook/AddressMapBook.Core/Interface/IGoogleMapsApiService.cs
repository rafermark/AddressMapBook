using AddressMapBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AddressMapBook.Core.Interface
{
    public interface IGoogleMapsApiService
    {
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<PlaceResult> GetPlaceDetails(string placeId);
        //Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude);
        Task<Geometry> GetCurrentLocation();
    }
}
