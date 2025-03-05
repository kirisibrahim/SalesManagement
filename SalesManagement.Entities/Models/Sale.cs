using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Entities.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        public Product? Product { get; set; }

        public int? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
