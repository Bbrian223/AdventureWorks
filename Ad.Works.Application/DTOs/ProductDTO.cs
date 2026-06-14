using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ad.Works.Application.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public decimal ListPrice { get; set; }
        public decimal? Weight { get; set; }

        public string? Model { get; set; }
        public string? Subcategory { get; set; }

    }

    public class ProductListDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string? Color { get; set; }
        public decimal ListPrice { get; set; }
    }

    public class ProductUpdateDTO
    {
        [Required]
        [StringLength(50,MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(15)]
        public string? Color { get; set; }

        [StringLength(5)]
        public string? Size { get; set; }
        
        [Range(0.00, 999999.99)]
        public decimal? Weight { get; set; }

        [Required]
        public decimal ListPrice { get; set; }

    }

    public class ProductCreateDto 
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string ProductNumber { get; set; }

        [StringLength(15)]
        public string? Color { get; set; }

        [StringLength(5)]
        public string? Size { get; set; }

        [Range(0.00, 999999.99)]
        public decimal? Weight { get; set; }

        [Required]
        public decimal ListPrice { get; set; }
    }
}
