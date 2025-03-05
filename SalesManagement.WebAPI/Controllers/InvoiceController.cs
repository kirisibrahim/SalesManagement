using Microsoft.AspNetCore.Mvc;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Business.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SalesManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null)
                return NotFound();
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdInvoice = await _invoiceService.CreateAsync(invoiceDto);
            return CreatedAtAction(nameof(GetById), new { id = createdInvoice.Id }, createdInvoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InvoiceDto invoiceDto)
        {
            var updatedInvoice = await _invoiceService.UpdateAsync(invoiceDto);
            return Ok(updatedInvoice);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _invoiceService.DeleteAsync(id);
            return NoContent();
        }
    }
}

