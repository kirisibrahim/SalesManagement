using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.DataAccess.Context;
using SalesManagement.DataAccess.Repositories.Interfaces;
using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesManagement.DataAccess.Repositories.Implementations
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SalesManagementDbContext _context;

        public SaleRepository(SalesManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _context.Sales.FindAsync(id);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        public async Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
            }
        }
    }
}
