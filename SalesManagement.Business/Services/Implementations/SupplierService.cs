using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Exceptions;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.DataAccess.UnitOfWork;
using SalesManagement.Entities.Models;

namespace SalesManagement.Business.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<SupplierDto> _validator;

        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<SupplierDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _unitOfWork.SupplierRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto> GetByIdAsync(int id)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id);
            if (supplier == null)
                throw new NotFoundException("Tedarikçi bulunamadı.");

            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<SupplierDto> CreateAsync(SupplierDto supplierDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(supplierDto);
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _unitOfWork.SupplierRepository.AddAsync(supplier);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task<SupplierDto> UpdateAsync(SupplierDto supplierDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(supplierDto);
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(supplierDto.Id);
            if (supplier == null)
                throw new NotFoundException("Tedarikçi bulunamadı.");

            _mapper.Map(supplierDto, supplier);
            await _unitOfWork.SupplierRepository.UpdateAsync(supplier);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SupplierDto>(supplier);
        }

        public async Task DeleteAsync(int id)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id);
            if (supplier == null)
                throw new NotFoundException("Tedarikçi bulunamadı.");

            await _unitOfWork.SupplierRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
