using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(int id);
    }
}
