using SalesManagement.Business.DTOs;
using SalesManagement.Entities.Models;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        Task<User?> ValidateUserAsync(LoginDto loginDto);
    }
}

