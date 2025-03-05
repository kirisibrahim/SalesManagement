using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.DataAccess.UnitOfWork;
using SalesManagement.Entities.Models;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesManagement.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<User?> ValidateUserAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAndPasswordAsync(loginDto.Username, loginDto.PasswordHash);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.PasswordHash, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
