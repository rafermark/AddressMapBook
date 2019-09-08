using AddressMapBook.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AddressMapBook.Core.Service
{
    class AccessManagerService : IAccessManager
    {
        public async Task<bool> Insert<T>(T entity)
        {
            return await CoreApp.Database.Conn.InsertAsync(entity) > 0;
        }

        public async Task<bool> Update<T>(T entity)
        {
            return await CoreApp.Database.Conn.UpdateAsync(entity) > 0;
        }

        public async Task<bool> Delete<T>(T entity)
        {
            return await CoreApp.Database.Conn.DeleteAsync(entity) > 0;
        }

        public async Task<bool> InsertAll<T>(IEnumerable<T> entities)
        {
            return await CoreApp.Database.Conn.InsertAllAsync(entities) > 0;
        }

        public async Task<bool> UpdateAll<T>(IEnumerable<T> entities)
        {
            return await CoreApp.Database.Conn.UpdateAllAsync(entities) > 0;
        }
    }
}
