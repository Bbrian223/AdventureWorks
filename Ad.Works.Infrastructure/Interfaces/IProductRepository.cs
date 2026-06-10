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
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<Product> GetAsync(int id);
    }
}