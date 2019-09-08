using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AddressMapBook.Core.Interface
{
    public interface IAccessManager
    {
        Task<bool> Insert<T>(T entity);
        Task<bool> Update<T>(T entity);
        Task<bool> Delete<T>(T entity);
        Task<bool> InsertAll<T>(IEnumerable<T> entities);
        Task<bool> UpdateAll<T>(IEnumerable<T> entities);
    }
}
