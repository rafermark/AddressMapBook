using AddressMapBook.Core.Event;
using AddressMapBook.Core.Interface;
using AddressMapBook.Core.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressMapBook.Core.Service
{
    class ConnectionService : IConnection
    {
        public ConnectionService()
        {

        }

        public void CheckConnectivity()
        {
            if (!CrossConnectivity.IsSupported)
                return;

            if (!CrossConnectivity.Current.IsConnected)
                throw new NoInternetConnectionException { ExceptionMessage = "No internet connection" };
        }

        public bool IsConnected()
        {
            if (!CrossConnectivity.IsSupported)
                return true;


            return CrossConnectivity.Current.IsConnected;
        }
    }
}
