using Microsoft.AspNetCore.Mvc;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Business.DTOs;
using System.Threading.Tasks;
using SalesManagement.Business.Services.Implementations;

namespace SalesManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.CreateAsync(productDto);
            return Ok();
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest("Product ID mismatch");

            await _productService.UpdateAsync(productDto);
            if (productDto == null)
                return NotFound();

            return Ok();
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
