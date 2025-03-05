using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Business.DTOs;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> AuthenticateAsync(LoginDto loginDto);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserDto userDto);
        Task<UserDto> UpdateAsync(UserDto userDto);
        Task DeleteAsync(int id);
    }
}
