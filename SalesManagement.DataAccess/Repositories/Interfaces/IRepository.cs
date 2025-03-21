using SalesManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        System.Threading.Tasks.Task AddAsync(T entity);
        System.Threading.Tasks.Task UpdateAsync(T entity);
        System.Threading.Tasks.Task DeleteAsync(int id);
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string passwordHash);
        Task<Role> GetRoleByNameAsync(string roleName);
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
    }
}
