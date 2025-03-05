using Microsoft.AspNetCore.Mvc;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Business.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SalesManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCategory = await _categoryService.CreateAsync(categoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto categoryDto)
        {
            var updatedCategory = await _categoryService.UpdateAsync(categoryDto);
            return Ok(updatedCategory);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
