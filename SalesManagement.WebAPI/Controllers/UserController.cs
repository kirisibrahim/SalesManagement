using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Services.Interfaces;
using System.Threading.Tasks;

namespace SalesManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/user/{id}
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // POST: api/user
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _userService.CreateAsync(userDto);
            return Ok(createdUser);
        }

        // PUT: api/user/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id)
                return BadRequest("User ID mismatch");

            var updatedUser = await _userService.UpdateAsync(userDto);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        // DELETE: api/user/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
