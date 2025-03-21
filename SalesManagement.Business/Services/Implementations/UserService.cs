using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Exceptions;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.DataAccess.UnitOfWork;
using SalesManagement.Entities.Models;

namespace SalesManagement.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UserDto> _userValidator;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UserDto> userValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userValidator = userValidator;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            // Kullanıcıların her birini dönüştürüp RoleName'i ekleyelim
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            foreach (var userDto in userDtos)
            {
                // RoleId'yi alıp RoleName olarak ayarlıyoruz
                var role = await _unitOfWork.RoleRepository.GetByIdAsync(userDto.RoleId);
                userDto.RoleName = role?.Name ?? "Bilinmiyor";  // Eğer rol bulunamazsa "Bilinmiyor" olarak ayarla
            }

            return userDtos;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            var userDto = _mapper.Map<UserDto>(user);

            // RoleName'i dolduruyoruz
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(userDto.RoleId);
            userDto.RoleName = role?.Name ?? "Bilinmiyor";  // Eğer rol bulunamazsa "Bilinmiyor" olarak ayarla

            return userDto;
        }



        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            var validationResult = await _userValidator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = _mapper.Map<User>(userDto);

            // Eğer kullanıcı rolü belirlenmemişse, varsayılan olarak "User" rolünü ata
            if (user.RoleId == 0)
            {
                var defaultRole = await _unitOfWork.RoleRepository.GetRoleByNameAsync("User");
                if (defaultRole != null)
                    user.RoleId = defaultRole.Id; // Varsayılan rolü ata
            }
            else
            {
                var role = await _unitOfWork.RoleRepository.GetByIdAsync(user.RoleId);
                if (role == null)
                    throw new NotFoundException("Rol bulunamadı."); // Eğer verilen RoleId yanlışsa hata fırlat
            }

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            // Kullanıcı DTO'sunu oluştur
            var createdUserDto = _mapper.Map<UserDto>(user);

            // Kullanıcının rolünü veritabanından çek ve RoleName'i ata
            var assignedRole = await _unitOfWork.RoleRepository.GetByIdAsync(user.RoleId);
            if (assignedRole != null)
                createdUserDto.RoleName = assignedRole.Name; // DTO içindeki RoleName alanını doldur

            return createdUserDto;
        }




        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            var validationResult = await _userValidator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userDto.Id);
            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            _mapper.Map(userDto, user);
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı.");

            await _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<UserDto> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAndPasswordAsync(loginDto.Username, loginDto.PasswordHash);
            if (user == null)
                return null;

            // Eğer user.Role null ise, hata mesajı döndür
            if (user.Role == null)
                throw new NotFoundException("Kullanıcının rolü bulunamadı.");

            // Kullanıcı bilgilerini ve rolünü al
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                RoleId = user.RoleId,
                RoleName = user.Role.Name,
                Role = new RoleDto
                {
                    Id = user.Role.Id,
                    Name = user.Role.Name
                }
            };
        }

        public Task<UserDto> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
