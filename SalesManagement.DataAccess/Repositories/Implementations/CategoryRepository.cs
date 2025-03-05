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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SalesManagementDbContext _context;

        public CategoryRepository(SalesManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
        }
    }
}
