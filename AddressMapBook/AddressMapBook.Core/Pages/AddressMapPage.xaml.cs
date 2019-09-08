using AddressMapBook.Core.Event;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AddressMapBook.Core.Pages
{
    public partial class AddressMapPage
    {
        public AddressMapPage()
        {
            InitializeComponent();
            


            //_token = _messenger.Subscribe<AddressResult>((addr) =>
            //{
            //    var position = new Position(addr.Address.Place.Result.Geometry.Location.Latitude, addr.Address.Place.Result.Geometry.Location.Longitude); // Latitude, Longitude
            //    var pin = new Pin
            //    {
            //        Type = PinType.Place,
            //        Position = position,
            //        Label = "custom pin",
            //        Address = "custom detail info"
            //    };
            //    MyMap.Pins.Add(pin);
            //});
        }


    }
}