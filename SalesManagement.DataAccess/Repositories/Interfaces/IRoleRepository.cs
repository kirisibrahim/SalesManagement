using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllAsync();
        System.Threading.Tasks.Task AddAsync(Role role);
        System.Threading.Tasks.Task UpdateAsync(Role role);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
