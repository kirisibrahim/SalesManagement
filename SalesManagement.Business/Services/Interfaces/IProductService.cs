using SalesManagement.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductDto productDto);
        Task<ProductDto> UpdateAsync(ProductDto productDto);
        Task DeleteAsync(int id);

    }
}
