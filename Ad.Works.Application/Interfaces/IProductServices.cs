using Ad.Works.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ad.Works.Application.Interfaces
{
    public interface IProductServices
    {
        public Task<IEnumerable<ProductDTO>> GetAllAsync();

        public Task<ProductDTO> GetAsync(int id);
    }
}
