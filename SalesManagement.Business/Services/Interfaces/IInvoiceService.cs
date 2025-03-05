using SalesManagement.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task<InvoiceDto> GetByIdAsync(int id);
        Task<InvoiceDto> CreateAsync(InvoiceDto invoiceDto);
        Task<InvoiceDto> UpdateAsync(InvoiceDto invoiceDto);
        Task DeleteAsync(int id);
    }
}
