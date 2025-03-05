using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface IStockMovementService
    {
        Task<IEnumerable<StockMovementDto>> GetAllAsync();
        Task<StockMovementDto> GetByIdAsync(int id);
        Task<StockMovementDto> CreateAsync(StockMovementDto stockMovementDto);
        Task<StockMovementDto> UpdateAsync(StockMovementDto stockMovementDto);
        Task DeleteAsync(int id);
    }
}
