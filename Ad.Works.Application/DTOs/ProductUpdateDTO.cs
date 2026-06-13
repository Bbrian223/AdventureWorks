using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad.Works.Application.DTOs
{
    public class ProductUpdateDTO
    {
        public string Name { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal? Weight { get; set; }
        public decimal ListPrice { get; set; }

    }
}
