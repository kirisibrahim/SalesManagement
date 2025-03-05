using Microsoft.AspNetCore.Mvc;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Business.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SalesManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementController : ControllerBase
    {
        private readonly IStockMovementService _stockMovementService;

        public StockMovementController(IStockMovementService stockMovementService)
        {
            _stockMovementService = stockMovementService;
        }

        // GET: api/stockmovement
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stockMovements = await _stockMovementService.GetAllAsync();
            return Ok(stockMovements);
        }

        // GET: api/stockmovement/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stockMovement = await _stockMovementService.GetByIdAsync(id);
            if (stockMovement == null)
                return NotFound();
            return Ok(stockMovement);
        }

        // POST: api/stockmovement
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StockMovementDto stockMovementDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _stockMovementService.CreateAsync(stockMovementDto);
            return Ok();
        }

        // PUT: api/stockmovement/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StockMovementDto stockMovementDto)
        {
            if (id != stockMovementDto.Id)
                return BadRequest("StockMovement ID mismatch");

            await _stockMovementService.UpdateAsync(stockMovementDto);
            if (stockMovementDto == null)
                return NotFound();

            return Ok();
        }

        // DELETE: api/stockmovement/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stockMovement = await _stockMovementService.GetByIdAsync(id);
            if (stockMovement == null)
                return NotFound();

            await _stockMovementService.DeleteAsync(id);
            return NoContent();
        }
    }
}

