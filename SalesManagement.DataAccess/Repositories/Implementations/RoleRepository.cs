using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.DataAccess.Context;
using SalesManagement.DataAccess.Repositories.Interfaces;
using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesManagement.DataAccess.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SalesManagementDbContext _context;

        public RoleRepository(SalesManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async System.Threading.Tasks.Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
            }
        }
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Set<Role>().FirstOrDefaultAsync(r => r.Name == roleName);
        }

    }
}
