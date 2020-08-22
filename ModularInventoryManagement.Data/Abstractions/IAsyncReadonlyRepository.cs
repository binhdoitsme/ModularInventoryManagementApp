using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModularInventoryManagement.Data.Abstractions
{
    public interface IAsyncReadonlyRepository<T> where T : class
    {
        Task<IEnumerable<T>> FindAll();
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> condition);
        Task<T> FindFirst(Expression<Func<T, bool>> condition);
        Task<T> FindById(int id);
        Task<bool> Exists(T item);
    }
}
