using AddressMapBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AddressMapBook.Core.Interface
{
    public interface IGoogleMapTableAccess : IAccessManager
    {
        Task<List<GoogleMapTables>> GetMapTables();
    }
}
