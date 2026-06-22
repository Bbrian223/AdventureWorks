using Ad.Works.Application.DTOs;
using Ad.Works.Application.Interfaces;
using Ad.Works.Infrastructure.Interfaces;
using Ad.Works.Domain.Entities;
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

        public async Task<IEnumerable<ProductListDTO>> GetListAsync(int cursor, int p_size)
        {
            var nList = new List<ProductListDTO>();

            if(cursor < 0)
                throw new Exception("invalid cursor");

            if(p_size < 5 || p_size > 20)
                throw new Exception("invalid page size.");

            var list = await _repository.GetListAsync(cursor,p_size);

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

        public async Task<ProductDTO> UpdateAsync(int id, ProductUpdateDTO prod)
        {
            if (await _repository.ExistById(id))
                throw new Exception("ID not fount. Update canceled");

            await _repository.UpdateAsync(new Product
            {
                ProductId = id,
                Name = prod.Name,
                Color = prod.Color,
                Size = prod.Size,
                Weight = prod.Weight,
                ListPrice= prod.ListPrice,
            });

            return await GetAsync(id);
        }

        public async Task DiscontinuedAsync(int id)
        {
            if (await _repository.ExistById(id))
                throw new Exception("ID not found. Update canceled");

            await _repository.DiscontinuedAsync(id);
        }

        public async Task<ProductDTO> CreateAsync(ProductCreateDto entity)
        {
            if (await _repository.ExistByProdNumber(entity.ProductNumber))
                throw new Exception("Product number already exists");

            var prod = await _repository.CreateAsync(new Product
            {
                Name = entity.Name,
                ProductNumber = entity.ProductNumber,
                Color = string.IsNullOrWhiteSpace(entity.Color) ? null : entity.Color,
                Size = string.IsNullOrWhiteSpace(entity.Size) ? null : entity.Size,
                Weight = entity.Weight,
                ListPrice = entity.ListPrice,

                SafetyStockLevel = 100,
                ReorderPoint = 50,
                StandardCost = 0,
                DaysToManufacture = 0,
                SellStartDate = DateTime.UtcNow
            });

            return await GetAsync(prod.ProductId);

        }
    }
}
