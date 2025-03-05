using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Exceptions;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Business.Validators;
using SalesManagement.DataAccess.UnitOfWork;
using SalesManagement.Entities.Models;

namespace SalesManagement.Business.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaleValidator _saleValidator;

        public SaleService(IUnitOfWork unitOfWork, IMapper mapper, SaleValidator saleValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saleValidator = saleValidator;
        }

        public async Task<IEnumerable<SaleDto>> GetAllAsync()
        {
            var sales = await _unitOfWork.SaleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SaleDto>>(sales);
        }

        public async Task<SaleDto> GetByIdAsync(int id)
        {
            var sale = await _unitOfWork.SaleRepository.GetByIdAsync(id);
            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");

            return _mapper.Map<SaleDto>(sale);
        }

        public async Task<SaleDto> CreateAsync(SaleDto saleDto)
        {
            var validationResult = await _saleValidator.ValidateAsync(saleDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Sale>(saleDto);
            await _unitOfWork.SaleRepository.AddAsync(sale);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SaleDto>(sale);
        }

        public async Task<SaleDto> UpdateAsync(SaleDto saleDto)
        {
            var sale = await _unitOfWork.SaleRepository.GetByIdAsync(saleDto.Id);
            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");

            _mapper.Map(saleDto, sale);
            await _unitOfWork.SaleRepository.UpdateAsync(sale);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<SaleDto>(sale);
        }

        public async Task DeleteAsync(int id)
        {
            var sale = await _unitOfWork.SaleRepository.GetByIdAsync(id);
            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");

            await _unitOfWork.SaleRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
