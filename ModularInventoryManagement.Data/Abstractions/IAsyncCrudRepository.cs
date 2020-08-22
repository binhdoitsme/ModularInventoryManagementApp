using System;
using System.Collections.Generic;
using System.Text;

namespace ModularInventoryManagement.Data.Abstractions
{
    public interface IAsyncCrudRepository<T> : IAsyncReadonlyRepository<T>,
                                                IAsyncMutableRepository<T>
                                                where T : class
    {
    }
}
