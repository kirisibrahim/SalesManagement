using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetByIdAsync(int id);
        Task<IEnumerable<Supplier>> GetAllAsync();
        System.Threading.Tasks.Task AddAsync(Supplier supplier);
        System.Threading.Tasks.Task UpdateAsync(Supplier supplier);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
