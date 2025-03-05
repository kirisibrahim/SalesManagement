using Microsoft.AspNetCore.Mvc;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Business.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SalesManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: api/supplier
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _supplierService.GetAllAsync();
            return Ok(suppliers);
        }

        // GET: api/supplier/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }

        // POST: api/supplier
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierDto supplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _supplierService.CreateAsync(supplierDto);
            return Ok();
        }

        // PUT: api/supplier/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierDto supplierDto)
        {
            if (id != supplierDto.Id)
                return BadRequest("Supplier ID mismatch");

            await _supplierService.UpdateAsync(supplierDto);
            if (supplierDto == null)
                return NotFound();

            return Ok();
        }

        // DELETE: api/supplier/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier == null)
                return NotFound();

            await _supplierService.DeleteAsync(id);
            return NoContent();
        }
    }
}

