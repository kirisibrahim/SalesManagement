using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface IStockMovementRepository
    {
        Task<StockMovement?> GetByIdAsync(int id);
        Task<IEnumerable<StockMovement>> GetAllAsync();
        System.Threading.Tasks.Task AddAsync(StockMovement stockMovement);
        System.Threading.Tasks.Task UpdateAsync(StockMovement stockMovement);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
