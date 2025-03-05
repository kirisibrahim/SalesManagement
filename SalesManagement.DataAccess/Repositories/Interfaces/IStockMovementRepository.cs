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
        Task AddAsync(StockMovement stockMovement);
        Task UpdateAsync(StockMovement stockMovement);
        Task DeleteAsync(int id);
    }
}
