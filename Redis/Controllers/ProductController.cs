using ApiRedisSample.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRedisSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get a product by id. First checks Redis; if not found, fetches from DB and caches it.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _service.GetProductAsync(id);
            if (product is null) return NotFound();
            return Ok(product);
        }
    }
}
