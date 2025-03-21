using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.DataAccess.Repositories.Interfaces;
using SalesManagement.Entities.Models;

namespace SalesManagement.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Supplier> SupplierRepository { get; }
        IRepository<StockMovement> StockMovementRepository { get; }
        IRepository<Sale> SaleRepository { get; }
        IRepository<Invoice> InvoiceRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<TasksTask> TasksTaskRepository { get; }

        Task<int> CompleteAsync();
    }
}
