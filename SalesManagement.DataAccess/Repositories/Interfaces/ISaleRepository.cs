using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(int id);
        Task<IEnumerable<Sale>> GetAllAsync();
        System.Threading.Tasks.Task AddAsync(Sale sale);
        System.Threading.Tasks.Task UpdateAsync(Sale sale);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
