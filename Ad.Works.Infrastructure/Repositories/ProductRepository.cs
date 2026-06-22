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

        #region GET LIST PROD
        public async Task<IEnumerable<Product>> GetListAsync(int cursor, int p_size)
        {
            var list = await _context.Products
                .Select(u => new Product {
                    ProductId = u.ProductId,
                    Name = u.Name,
                    ProductNumber = u.ProductNumber,
                    Color = u.Color,
                    ListPrice = u.ListPrice,
                })
                .Where(u => u.ProductId > cursor )
                .OrderBy(u => u.ProductId)
                .Take(p_size)
                .ToListAsync();

            if (!list.Any())
                return null;

            return list;
        }
        #endregion

        #region GET PROD BY ID
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

        #region UPDATE PROD
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Attach(product);
            _context.Entry(product).Property(p => p.Name).IsModified = true;
            _context.Entry(product).Property(p => p.Color).IsModified = true;
            _context.Entry(product).Property(p => p.Size).IsModified = true;
            _context.Entry(product).Property(p => p.Weight).IsModified = true;
            _context.Entry(product).Property(p => p.ListPrice).IsModified = true;

            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }
        #endregion

        #region DISCONTINUED PROD
        public async Task DiscontinuedAsync(int id)
        {
            var rows = await _context.Products
                .Where(p => p.ProductId == id)
                .ExecuteUpdateAsync(s => s.SetProperty(
                    p => p.DiscontinuedDate, DateTime.Now)
                );
        }
        #endregion

        public async Task<bool> ExistById(int id) {
            return await _context.Products
                .AnyAsync(p => p.ProductId == id);
        }

        public async Task<bool> ExistByProdNumber(string number)
        {
            return await _context.Products
                .AnyAsync(p => p.ProductNumber == number);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
