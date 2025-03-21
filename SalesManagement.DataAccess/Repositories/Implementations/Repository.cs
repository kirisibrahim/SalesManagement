
using SalesManagement.DataAccess.Context;
using SalesManagement.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SalesManagementDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(SalesManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async System.Threading.Tasks.Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string passwordHash)
        {
            return await _context.Users
                 .Where(u => u.Username == username && u.PasswordHash == passwordHash)
                 .Include(u => u.Role)  // Role bilgisini de yükle
                 .FirstOrDefaultAsync();
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Set<Role>().FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

    }
}
