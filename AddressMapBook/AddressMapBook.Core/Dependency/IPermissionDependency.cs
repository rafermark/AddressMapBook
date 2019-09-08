using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AddressMapBook.Core.Dependency
{
    public interface IPermissionDependency
    {
        Task<bool> LocationPermission();
    }
}
