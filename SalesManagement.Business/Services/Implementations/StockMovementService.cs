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
    public class StockMovementService : IStockMovementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<StockMovementDto> _stockMovementValidator;

        public StockMovementService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<StockMovementDto> stockMovementValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stockMovementValidator = stockMovementValidator;
        }

        public async Task<StockMovementDto> GetByIdAsync(int id)
        {
            var stockMovement = await _unitOfWork.StockMovementRepository.GetByIdAsync(id);
            if (stockMovement == null)
                throw new NotFoundException("Stok hareketi bulunamadı.");

            return _mapper.Map<StockMovementDto>(stockMovement);
        }

        public async Task<IEnumerable<StockMovementDto>> GetAllAsync()
        {
            var stockMovements = await _unitOfWork.StockMovementRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StockMovementDto>>(stockMovements);
        }

        public async Task<StockMovementDto> CreateAsync(StockMovementDto stockMovementDto)
        {
            var validationResult = await _stockMovementValidator.ValidateAsync(stockMovementDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var stockMovement = _mapper.Map<StockMovement>(stockMovementDto);
            await _unitOfWork.StockMovementRepository.AddAsync(stockMovement);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<StockMovementDto>(stockMovement);
        }

        public async Task<StockMovementDto> UpdateAsync(StockMovementDto stockMovementDto)
        {
            var validationResult = await _stockMovementValidator.ValidateAsync(stockMovementDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var stockMovement = await _unitOfWork.StockMovementRepository.GetByIdAsync(stockMovementDto.Id);
            if (stockMovement == null)
                throw new NotFoundException("Stok hareketi bulunamadı.");

            _mapper.Map(stockMovementDto, stockMovement);
            await _unitOfWork.StockMovementRepository.UpdateAsync(stockMovement);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<StockMovementDto>(stockMovement);
        }

        public async Task DeleteAsync(int id)
        {
            var stockMovement = await _unitOfWork.StockMovementRepository.GetByIdAsync(id);
            if (stockMovement == null)
                throw new NotFoundException("Stok hareketi bulunamadı.");

            await _unitOfWork.StockMovementRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
