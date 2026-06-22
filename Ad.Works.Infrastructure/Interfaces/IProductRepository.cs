using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ad.Works.Domain.Entities;

namespace Ad.Works.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetListAsync(int cursor, int p_size);
        public Task<Product> GetAsync(int id);

        public Task UpdateAsync(Product product);

        public Task DiscontinuedAsync(int id);

        public Task<Product> CreateAsync(Product product);

        public Task<bool> ExistById(int id);

        public Task<bool> ExistByProdNumber(string num);
    }
}