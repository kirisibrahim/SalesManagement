using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Business.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

    }
}
