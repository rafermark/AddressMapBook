using AddressMapBook.Core.Models;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressMapBook.Core.Event
{
    class EventAggregator
    {
    }

    public class ConnectionChanged : MvxMessage
    {
        public ConnectionChanged(object sender, bool connected) : base(sender)
        {
            Connected = connected;
        }

        public bool Connected { get; private set; }
    }

    public class AddressResult : MvxMessage
    {
        public AddressResult(object sender, GoogleMapTables address) : base(sender)
        {
            Address = address;
        }

        public GoogleMapTables Address { get; set; }
    }
}
