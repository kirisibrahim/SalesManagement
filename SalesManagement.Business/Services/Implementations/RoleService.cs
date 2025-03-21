using AutoMapper;
using FluentValidation;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Exceptions;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Business.Validators;
using SalesManagement.DataAccess.UnitOfWork;
using SalesManagement.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesManagement.Business.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RoleValidator _roleValidator;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, RoleValidator roleValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleValidator = roleValidator;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            if (role == null)
                throw new KeyNotFoundException("Role not found.");

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> CreateAsync(RoleDto roleDto)
        {
            var validationResult = await _roleValidator.ValidateAsync(roleDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var role = _mapper.Map<Role>(roleDto);

            await _unitOfWork.RoleRepository.AddAsync(role);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> UpdateAsync(RoleDto roleDto)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleDto.Id);
            if (role == null)
                throw new KeyNotFoundException("Role not found.");

            _mapper.Map(roleDto, role);

            await _unitOfWork.RoleRepository.UpdateAsync(role);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<RoleDto>(role);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            if (role == null)
                throw new KeyNotFoundException("Role not found.");

            // Rolü kullanan kullanıcıları bul
            var usersWithRole = await _unitOfWork.UserRepository.GetWhereAsync(u => u.RoleId == id);

            // Varsayılan Role ID'sini belirle (örneğin: 1 = "User")
            int defaultRoleId = 1;

            // Kullanıcıların RoleId'sini güncelle
            foreach (var user in usersWithRole)
            {
                user.RoleId = defaultRoleId;
                await _unitOfWork.UserRepository.UpdateAsync(user);
            }

            // Değişiklikleri kaydet
            await _unitOfWork.CompleteAsync();

            // Şimdi rolü güvenle silebiliriz
            await _unitOfWork.RoleRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

    }
}
