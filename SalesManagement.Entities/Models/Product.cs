﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedDate { get; set; }

        public Category? Category { get; set; }
        public Supplier? Supplier { get; set; }
        public string Description { get; set; } = string.Empty;
        public required ICollection<Sale> Sales { get; set; }
    }
}
