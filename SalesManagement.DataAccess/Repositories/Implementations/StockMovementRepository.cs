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
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly SalesManagementDbContext _context;

        public StockMovementRepository(SalesManagementDbContext context)
        {
            _context = context;
        }

        public async Task<StockMovement?> GetByIdAsync(int id)
        {
            return await _context.StockMovements.FindAsync(id);
        }

        public async Task<IEnumerable<StockMovement>> GetAllAsync()
        {
            return await _context.StockMovements.ToListAsync();
        }

        public async Task AddAsync(StockMovement stockMovement)
        {
            await _context.StockMovements.AddAsync(stockMovement);
        }

        public async Task UpdateAsync(StockMovement stockMovement)
        {
            _context.StockMovements.Update(stockMovement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stockMovement = await _context.StockMovements.FindAsync(id);
            if (stockMovement != null)
            {
                _context.StockMovements.Remove(stockMovement);
            }
        }
    }
}
