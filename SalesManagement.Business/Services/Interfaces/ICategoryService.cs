using SalesManagement.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryDto categoryDto);
        Task<CategoryDto> UpdateAsync(CategoryDto categoryDto);
        Task DeleteAsync(int id);
    }
}
