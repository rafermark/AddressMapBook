using System;
using System.Collections.Generic;
using System.Text;

namespace AddressMapBook.Core.Interface
{
    public interface IConnection
    {
        bool IsConnected();
        void CheckConnectivity();
    }
}
