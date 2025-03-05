using System;
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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryDto> _categoryValidator;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CategoryDto> categoryValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryValidator = categoryValidator;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Kategori bulunamadı.");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto categoryDto)
        {
            var validationResult = await _categoryValidator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateAsync(CategoryDto categoryDto)
        {
            var validationResult = await _categoryValidator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryDto.Id);
            if (category == null)
                throw new NotFoundException("Kategori bulunamadı.");

            _mapper.Map(categoryDto, category);
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Kategori bulunamadı.");

            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
