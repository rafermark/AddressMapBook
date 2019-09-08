using System;
using System.Collections.Generic;
using System.Text;

namespace AddressMapBook.Core.Dependency
{
    public interface IQuickMessageDependency
    {
        void ShowToastMessage(string message);
    }
}
