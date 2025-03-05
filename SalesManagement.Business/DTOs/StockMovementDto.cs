using SalesManagement.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Business.DTOs
{
    public class StockMovementDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
        public StockMovementType MovementType { get; set; }
    }
}
