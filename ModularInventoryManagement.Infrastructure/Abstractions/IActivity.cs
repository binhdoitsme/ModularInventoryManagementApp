using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModularInventoryManagement.Infrastructure.Abstractions
{
    public interface IActivity
    {
        void Perform();
    }

    public interface IActivity<T>
    {
        Task<T> PerformAsync(params object[] inputParams);
    }
}
