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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductDto> _productValidator;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ProductDto> productValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productValidator = productValidator;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("Ürün bulunamadı.");

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> CreateAsync(ProductDto productDto)
        {
            var validationResult = await _productValidator.ValidateAsync(productDto);
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateAsync(ProductDto productDto)
        {
            var validationResult = await _productValidator.ValidateAsync(productDto);
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productDto.Id);
            if (product == null)
                throw new NotFoundException("Ürün bulunamadı.");

            _mapper.Map(productDto, product);
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("Ürün bulunamadı.");

            await _unitOfWork.ProductRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
