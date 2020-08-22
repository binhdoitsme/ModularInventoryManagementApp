using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModularInventoryManagement.Data.Abstractions
{
    public interface IAsyncMutableRepository<T> where T : class
    {
        Task<T> Add(T item);
        Task Remove(T item);
        Task Update(T currentState, object newState);
    }
}
