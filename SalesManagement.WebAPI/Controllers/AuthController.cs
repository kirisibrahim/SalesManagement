﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Entities.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.AuthenticateAsync(loginDto);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            return Ok(new { token, refreshToken });
        }

        // Refresh Token ile JWT Yenileme
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Invalid refresh token.");

            var newToken = GenerateJwtToken(new UserDto { /* Yeni user bilgileri buraya alınmalı */ });
            return Ok(new { newToken });
        }

        private string GenerateJwtToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var role = user.Role;
            if (role == null)
            {
                throw new InvalidOperationException("User role is missing");
            }

            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, user.Username),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.Name, user.Username),
               new Claim(ClaimTypes.Role, role.Name), // Rol adı ekleniyor
               new Claim("userId", user.Id.ToString()), // userid claim ekleme
               new Claim("roleId", role.Id.ToString()) // roleid claim ekleme
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("user-info")]
        public IActionResult GetUserInfo()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var roleName = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst("userId")?.Value;
            var roleId = User.FindFirst("roleId")?.Value;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(roleName))
            {
                return Unauthorized("Kullanıcı bilgisi alınamadı.");
            }

            return Ok(new
            {
                Username = username,
                RoleName = roleName,
                UserId = userId,
                RoleId = roleId
            });
        }


        //Refresh Token Oluşturma
        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
