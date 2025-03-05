using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using SalesManagement.DataAccess.Configurations;

namespace SalesManagement.DataAccess.Context
{
    public class SalesManagementDbContext : DbContext
    {
        public SalesManagementDbContext(DbContextOptions<SalesManagementDbContext> options) : base(options){}
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesManagementDbContext).Assembly);  // Tüm konfigürasyonları uygulama
        }
    }
}
