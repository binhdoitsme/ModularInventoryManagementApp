using Microsoft.EntityFrameworkCore;
using ModularInventoryManagement.Data.Abstractions;
using ModularInventoryManagement.Data.Database;
using ModularInventoryManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModularInventoryManagement.Data.Repositories
{
    public class ReadonlyUserRepository : IAsyncReadonlyRepository<User>
    {
        private readonly InventoryManagementContext dbContext;

        public ReadonlyUserRepository()
        {
            dbContext = InventoryManagementContext.GetInstance();
        }

        public Task<bool> Exists(User item)
        {
            return dbContext.User.AnyAsync(user => user.Username == item.Username);
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            return await dbContext.User.ToListAsync();
        }

        public async Task<IEnumerable<User>> FindAll(Expression<Func<User, bool>> condition)
        {
            return await dbContext.User.Where(condition).ToListAsync();
        }

        public Task<User> FindFirst(Expression<Func<User, bool>> condition)
        {
            return dbContext.User
                        .Include(user => user.Role)
                            .ThenInclude(role => role.RolePermission)
                            .ThenInclude(rolePermissions => rolePermissions.Permission)
                        .Where(condition).FirstOrDefaultAsync();
        }

        public Task<User> FindById(int id)
        {
            return dbContext.User
                        .Include(user => user.Role)
                            .ThenInclude(role => role.RolePermission)
                            .ThenInclude(rolePermissions => rolePermissions.Permission)
                        .FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
