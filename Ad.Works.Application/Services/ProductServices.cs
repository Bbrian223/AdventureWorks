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

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            List<ProductDTO> nList = new List<ProductDTO>();

            var list = await _repository.GetAllAsync();

            foreach (var item in list) {
                nList.Add(new ProductDTO { 
                    ProductId = item.ProductId,
                    Name = item.Name,
                    Color = item.Color,
                    ProductNumber = item.ProductNumber,
                    SafetyStockLevel = item.SafetyStockLevel,
                    StandardCost = item.StandardCost
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
                Color = product.Color,
                ProductNumber= product.ProductNumber,
                SafetyStockLevel= product.SafetyStockLevel,
                StandardCost = product.StandardCost
            };

        }
    }
}
