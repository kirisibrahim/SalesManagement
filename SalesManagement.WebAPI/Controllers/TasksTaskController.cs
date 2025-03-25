using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.Entities.Models;
using System.Threading.Tasks;

namespace SalesManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksTaskController : ControllerBase
    {
        private readonly ITasksTaskService _tasksTaskService;
        private readonly IMapper _mapper;

        public TasksTaskController(ITasksTaskService tasksTaskService, IMapper mapper)
        {
            _tasksTaskService = tasksTaskService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTasksTaskAsync([FromBody] TasksTaskDto tasksTaskDto)
        {
            if (tasksTaskDto == null)
            {
                return BadRequest();
            }

            // Görevi oluştur
            var createdTask = await _tasksTaskService.CreateTasksTaskAsync(tasksTaskDto);

            //return CreatedAtAction(
            //    nameof(GetTasksTaskByIdAsync), // GetTasksTaskByIdAsync metodunu kullanarak detaylarını alıyoruz
            //    new { id = createdTask.Id }, // Parametre olarak görev ID'sini ekliyoruz
            //    createdTask); // Döndürülen görevi içerik olarak veriyoruz

            return Ok(createdTask); // Kaynağa erişim için url sağlamasına şuanlık ihtiyaç yokk
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasksTaskAsync(int id, [FromBody] UpdateTasksTaskDto updateTasksTaskDto)
        {
            if (updateTasksTaskDto == null)
                return BadRequest("Güncellenmek istenen görev bilgileri geçersiz.");

            var isUpdated = await _tasksTaskService.UpdateTasksTaskAsync(id, updateTasksTaskDto);
            if (!isUpdated)
                return NotFound("Görev bulunamadı.");

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasksTaskAsync(int id)
        {
            var isDeleted = await _tasksTaskService.DeleteTasksTaskAsync(id);
            if (!isDeleted)
                return NotFound("Görev bulunamadı.");

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllTasksTasksAsync()
        {
            var tasks = await _tasksTaskService.GetAllTasksTasksAsync();
            return Ok(tasks);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksTaskDto>> GetTasksTaskByIdAsync(int id)
        {
            var task = await _tasksTaskService.GetTasksTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksByUserId(int userId)
        {
            var tasks = await _tasksTaskService.GetTasksByUserIdAsync(userId);
            if (tasks == null || tasks.Count == 0)
            {
                return NotFound("Bu kullanıcıya atanmış görev bulunamadı.");
            }
            return Ok(tasks);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{tasksTaskId}/assign/{userId}")]
        public async Task<IActionResult> AssignUserToTasksTaskAsync(int tasksTaskId, int userId)
        {
            var isAssigned = await _tasksTaskService.AssignUserToTasksTaskAsync(tasksTaskId, userId);
            if (!isAssigned)
                return BadRequest("Kullanıcı atama işlemi başarısız.");

            return Ok("Kullanıcı başarıyla atandı.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{tasksTaskId}/remove/{userId}")]
        public async Task<IActionResult> RemoveUserFromTask(int tasksTaskId, int userId)
        {
            var result = await _tasksTaskService.RemoveUserFromTask(tasksTaskId, userId);
            if (!result)
                return NotFound("Kullanıcı görevde bulunamadı veya işlem başarısız oldu.");

            return Ok("Kullanıcı görevden başarıyla kaldırıldı.");
        }
    }
}
