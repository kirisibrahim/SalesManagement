using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerDto> _customerValidator;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CustomerDto> customerValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerValidator = customerValidator;
        }
        public async Task<CustomerDto> CreateAsync(CustomerDto customerDto)
        {
            var validationResult = await _customerValidator.ValidateAsync(customerDto);
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var customer = _mapper.Map<Customer>(customerDto);
            await _unitOfWork.CustomerRepository.AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Müşteri bulunamadı.");

            await _unitOfWork.CustomerRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Kategori bulunamadı.");

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> UpdateAsync(CustomerDto customerDto)
        {
            var validationResult = await _customerValidator.ValidateAsync(customerDto);
            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerDto.Id);
            if (customer == null)
                throw new NotFoundException("Müşteri bulunamadı.");

            _mapper.Map(customerDto, customer);
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
