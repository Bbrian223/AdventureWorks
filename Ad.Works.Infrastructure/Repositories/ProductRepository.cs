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

        #region GET ALL
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var list = await _context.Products.
                ToListAsync();

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
                .SingleOrDefaultAsync();

            return product;
        }
        #endregion
    }
}
