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
        public Task<IEnumerable<ProductListDTO>> GetListAsync();

        public Task<ProductDTO> GetAsync(int id);
    }
}
