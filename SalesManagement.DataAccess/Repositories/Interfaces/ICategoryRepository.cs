using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        System.Threading.Tasks.Task AddAsync(Category category);
        System.Threading.Tasks.Task UpdateAsync(Category category);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
