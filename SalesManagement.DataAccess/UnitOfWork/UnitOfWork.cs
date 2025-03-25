using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.DataAccess.Context;
using SalesManagement.DataAccess.Repositories.Implementations;
using SalesManagement.DataAccess.Repositories.Interfaces;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesManagementDbContext _context;

        public UnitOfWork(SalesManagementDbContext context)
        {
            _context = context;
            _productRepository = new Repository<Product>(_context);
            _categoryRepository = new Repository<Category>(_context);
            _supplierRepository = new Repository<Supplier>(_context);
            _saleRepository = new Repository<Sale>(_context);
            _invoiceRepository = new Repository<Invoice>(_context);
            _userRepository = new Repository<User>(_context);
            _roleRepository = new Repository<Role>(_context);
            _tasksTaskRepository = new Repository<TasksTask>(_context);
            _customerRepository = new Repository<Customer>(_context);
        }

        private IRepository<Product> _productRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Supplier> _supplierRepository;
        private IRepository<Sale> _saleRepository;
        private IRepository<Invoice> _invoiceRepository;
        private IRepository<User> _userRepository;
        private IRepository<Role> _roleRepository;
        private IRepository<TasksTask> _tasksTaskRepository;
        private IRepository<Customer> _customerRepository;

        public IRepository<Product> ProductRepository => _productRepository ??= new Repository<Product>(_context);
        public IRepository<Category> CategoryRepository => _categoryRepository ??= new Repository<Category>(_context);
        public IRepository<Supplier> SupplierRepository => _supplierRepository ??= new Repository<Supplier>(_context);
        public IRepository<Sale> SaleRepository => _saleRepository ??= new Repository<Sale>(_context);
        public IRepository<Invoice> InvoiceRepository => _invoiceRepository ??= new Repository<Invoice>(_context);
        public IRepository<User> UserRepository => _userRepository ??= new Repository<User>(_context);
        public IRepository<Role> RoleRepository => _roleRepository ??= new Repository<Role>(_context);
        public IRepository<TasksTask> TasksTaskRepository => _tasksTaskRepository ??= new Repository<TasksTask>(_context);
        public IRepository<Customer> CustomerRepository => _customerRepository ??= new Repository<Customer>(_context);
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
