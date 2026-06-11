using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad.Works.Application.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string ProductNumber { get; set; }
        public short SafetyStockLevel { get; set; }
        public decimal StandardCost { get; set; }
    }
}
