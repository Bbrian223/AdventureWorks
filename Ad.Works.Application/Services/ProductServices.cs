using Ad.Works.Application.DTOs;
using Ad.Works.Application.Interfaces;
using Ad.Works.Infrastructure.Interfaces;
using Ad.Works.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad.Works.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _repository;

        public ProductServices(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductListDTO>> GetListAsync()
        {
            var nList = new List<ProductListDTO>();

            var list = await _repository.GetListAsync();

            foreach (var item in list) {
                nList.Add(new ProductListDTO { 
                    ProductId = item.ProductId,
                    Name = item.Name,
                    ProductNumber = item.ProductNumber,
                    Color = item.Color,
                    ListPrice = item.ListPrice,
                });
            }

            return nList;
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            if (id <= 0)
                throw new Exception("invalid id");

            var product = await _repository.GetAsync(id);

            return new ProductDTO {
                ProductId = product.ProductId,
                Name = product.Name,
                ProductNumber = product.ProductNumber,
                Color = product.Color,
                Size = product.Size,
                ListPrice = product.ListPrice,
                Weight = product.Weight,
                Model = product.ProductModel?.Name ?? "Sin Modelo",
                Subcategory = product.ProductSubcategory?.Name ?? "Sin categoria"
            };

        }
    }
}
