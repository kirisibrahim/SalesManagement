using Microsoft.AspNetCore.Mvc;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Business.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SalesManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null)
                return NotFound();
            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleDto saleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSale = await _saleService.CreateAsync(saleDto);
            return CreatedAtAction(nameof(GetById), new { id = createdSale.Id }, createdSale);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaleDto saleDto)
        {
            var updatedSale = await _saleService.UpdateAsync(saleDto);
            return Ok(updatedSale);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _saleService.DeleteAsync(id);
            return NoContent();
        }
    }
}

