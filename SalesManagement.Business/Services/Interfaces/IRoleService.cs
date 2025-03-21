using SalesManagement.Business.DTOs;
using SalesManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<RoleDto> GetByIdAsync(int id);
        Task<RoleDto> CreateAsync(RoleDto roleDto);
        Task<RoleDto> UpdateAsync(RoleDto roleDto);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
