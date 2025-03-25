using AutoMapper;
using FluentValidation;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Exceptions;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.DataAccess.UnitOfWork;
using SalesManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesManagement.Business.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<InvoiceDto> _invoiceValidator;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<InvoiceDto> invoiceValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _invoiceValidator = invoiceValidator;
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _unitOfWork.InvoiceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public async Task<InvoiceDto> GetByIdAsync(int id)
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(id);
            if (invoice == null)
                throw new KeyNotFoundException("Fatura bulunamadı.");

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<InvoiceDto> CreateAsync(InvoiceDto invoiceDto)
        {
            var validationResult = await _invoiceValidator.ValidateAsync(invoiceDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var invoice = _mapper.Map<Invoice>(invoiceDto);
            await _unitOfWork.InvoiceRepository.AddAsync(invoice);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<InvoiceDto> UpdateAsync(InvoiceDto invoiceDto)
        {
            var validationResult = await _invoiceValidator.ValidateAsync(invoiceDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(invoiceDto.Id);
            if (invoice == null)
                throw new NotFoundException("Fatura bulunamadı.");

            _mapper.Map(invoiceDto, invoice);
            await _unitOfWork.InvoiceRepository.UpdateAsync(invoice);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(id);
            if (invoice == null)
                throw new KeyNotFoundException("Fatura bulunamadı.");

            await _unitOfWork.InvoiceRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
