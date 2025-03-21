using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        System.Threading.Tasks.Task AddAsync(Product product);
        System.Threading.Tasks.Task UpdateAsync(Product product);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
