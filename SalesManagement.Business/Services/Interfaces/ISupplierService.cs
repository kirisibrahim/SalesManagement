using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync();
        Task<SupplierDto> GetByIdAsync(int id);
        Task<SupplierDto> CreateAsync(SupplierDto supplierDto);
        Task<SupplierDto> UpdateAsync(SupplierDto supplierDto);
        Task DeleteAsync(int id);
    }
}
