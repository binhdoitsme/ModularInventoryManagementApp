using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.Infrastructure.Extensibility
{
    public interface IStateLocator
    {
        IStateLocator SetInitialState(IStateObject initialState);
        T Get<T>(string key) where T : class;
        void Put<T>(string key, T data) where T : class;
        void CopyTo(IStateObject target);
        void Clear();
    }
}
