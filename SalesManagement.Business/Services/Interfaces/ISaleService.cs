using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDto>> GetAllAsync();
        Task<SaleDto> GetByIdAsync(int id);
        Task<SaleDto> CreateAsync(SaleDto saleDto);
        Task<SaleDto> UpdateAsync(SaleDto saleDto);
        Task DeleteAsync(int id);
    }
}
