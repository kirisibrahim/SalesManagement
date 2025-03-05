using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesManagement.Entities.Enums;

namespace SalesManagement.Entities.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public StockMovementType MovementType { get; set; } // Giriş veya Çıkış
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;

        public Product? Product { get; set; }
    }
}
