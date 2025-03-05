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
    public class SupplierRepository : ISupplierRepository
    {
        private readonly SalesManagementDbContext _context;

        public SupplierRepository(SalesManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task AddAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
            }
        }
    }
}
