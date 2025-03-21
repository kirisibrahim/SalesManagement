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
        System.Threading.Tasks.Task AddAsync(Invoice invoice);
        System.Threading.Tasks.Task UpdateAsync(Invoice invoice);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
