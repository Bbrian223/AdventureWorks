using Ad.Works.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ad.Works.Domain.Entities;
using Ad.Works.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ad.Works.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AdventureWorksDbContext _context;

        public ProductRepository(AdventureWorksDbContext context)
        {
            _context = context;
        }

        #region GET LIST
        public async Task<IEnumerable<Product>> GetListAsync()
        {
            var list = await _context.Products
                .Select(u => new Product { 
                    ProductId = u.ProductId,
                    Name = u.Name,
                    ProductNumber = u.ProductNumber,
                    Color = u.Color,
                    ListPrice = u.ListPrice,
                })
                .OrderBy(u => u.ProductId)
                .ToListAsync();

            if (!list.Any())
                return null;

            return list;
        }
        #endregion

        #region GET BY ID
        public async Task<Product> GetAsync(int id)
        {
            var product = await _context.Products
                .Where(u => u.ProductId == id)
                .Include(u => u.ProductModel)
                .Include(u => u.ProductSubcategory)
                .SingleOrDefaultAsync();

            return product;
        }
        #endregion
    }
}
