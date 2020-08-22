using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.Infrastructure.Extensibility
{
    public interface IStateObject
    {
        event EventHandler<object> StateChanged;
        T GetState<T>(string key) where T : class;
        void PutState<T>(string key, T state) where T : class;
        void CopyTo(IStateObject target);
        void Clear();
    }
}
