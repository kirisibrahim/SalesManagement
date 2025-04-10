﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Business.DTOs
{
    public class SaleDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTimeOffset SaleDate { get; set; } = DateTimeOffset.UtcNow;
        public int? InvoiceId { get; set; }
    }
}
