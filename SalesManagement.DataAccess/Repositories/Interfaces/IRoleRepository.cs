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
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(int id);
    }
}
