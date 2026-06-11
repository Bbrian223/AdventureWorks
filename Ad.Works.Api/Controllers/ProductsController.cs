using Ad.Works.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllProducts(){
            try
            {
                var result = await _service.GetListAsync();

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


    }
}
