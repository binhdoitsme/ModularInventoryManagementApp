using ModularInventoryManagement.Data.Abstractions;
using ModularInventoryManagement.Data.Models;
using ModularInventoryManagement.Data.Repositories;
using ModularInventoryManagement.Infrastructure.Abstractions;
using System.Threading.Tasks;

namespace ModularInventoryManagement.AuthenticationManagement.Business
{
    abstract class AuthenticationActivity : IActivity<object>
    {
        protected readonly IAsyncReadonlyRepository<User> userRepository;

        protected AuthenticationActivity()
        {
            userRepository = new ReadonlyUserRepository();
        }

        public abstract Task<object> PerformAsync(params object[] inputParams);
    }
}
