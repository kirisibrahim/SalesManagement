using System;
using System.Linq;

namespace SalesManagement.Entities.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTimeOffset IssueDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal TotalAmount { get; set; }
        public ICollection<Sale>? Sales { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
