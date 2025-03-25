using SalesManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        System.Threading.Tasks.Task AddAsync(Customer customer);
        System.Threading.Tasks.Task UpdateAsync(Customer customer);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
