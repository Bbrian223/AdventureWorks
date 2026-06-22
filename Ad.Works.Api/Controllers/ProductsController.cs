using Ad.Works.Application.DTOs;
using Ad.Works.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ad.Works.Api.Controllers
{
    [ApiController]
    [Route("/API/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductServices _service;

        public ProductsController(IProductServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] int cursor = 0, 
            [FromQuery] int p_size = 10){
            try
            {
                var result = await _service.GetListAsync(cursor, p_size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var result = await _service.GetAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto prod)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.CreateAsync(prod);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message);
            }
        }

        [HttpPut("{id:int}/update")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO prod)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (id < 0)
                    return BadRequest("Invalid Id");

                var result = await _service.UpdateAsync(id,prod);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DiscontinuedProduct(int id)
        {
            try
            {
                if (id < 0)
                    return BadRequest("Invalid Id");

                await _service.DiscontinuedAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
