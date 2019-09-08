using AddressMapBook.Core.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressMapBook.Core.Database
{
    public class MapsDB
    {
        public readonly SQLiteAsyncConnection Conn;
        public MapsDB(string dbPath)
        {
            Conn = new SQLiteAsyncConnection(dbPath);
            CheckTables();
        }

        private void CheckTables()
        {
            Conn.CreateTableAsync<GoogleMapTables>().Wait();
        }

    }
}
