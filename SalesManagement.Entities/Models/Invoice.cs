using System;
using System.Linq;

namespace SalesManagement.Entities.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        public ICollection<Sale>? Sales { get; set; }
    }
}
