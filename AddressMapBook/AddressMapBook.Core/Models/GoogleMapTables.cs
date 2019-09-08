using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressMapBook.Core.Models
{
    [Table("GoogleMapTables")]
    public class GoogleMapTables : BindableBase
    {
        private string _placeId;
        public string PlaceId
        {
            get { return _placeId; }
            set { SetProperty(ref _placeId, value); }
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set { SetProperty(ref _latitude, value); }
        }

        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set { SetProperty(ref _longitude, value); }
        }

        private string _mainText;
        public string MainText
        {
            get { return _mainText; }
            set { SetProperty(ref _mainText, value); }
        }

        private string _secondaryText;
        public string SecondaryText
        {
            get { return _secondaryText; }
            set { SetProperty(ref _secondaryText, value); }
        }
    }
}
