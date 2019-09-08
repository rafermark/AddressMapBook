using AddressMapBook.Core.Interface;
using AddressMapBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AddressMapBook.Core.Service
{
    class GoogleMapTableAccessService : AccessManagerService, IGoogleMapTableAccess
    {
        public async Task<List<GoogleMapTables>> GetMapTables()
        {
            var ret = await CoreApp.Database.Conn.Table<GoogleMapTables>().ToListAsync();
            ret = ret ?? new List<GoogleMapTables>();
            return ret;
        }
    }
}
