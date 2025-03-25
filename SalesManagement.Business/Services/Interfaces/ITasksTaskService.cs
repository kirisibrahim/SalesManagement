using SalesManagement.Business.DTOs;
using SalesManagement.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesManagement.Business.Services.Interfaces
{
    public interface ITasksTaskService
    {
        Task<TasksTaskDto> CreateTasksTaskAsync(TasksTaskDto tasksTaskDto);
        Task<bool> UpdateTasksTaskAsync(int id, UpdateTasksTaskDto updateDto);
        Task<bool> DeleteTasksTaskAsync(int id);
        Task<IEnumerable<TasksTaskDto>> GetAllTasksTasksAsync();
        Task<TasksTaskDto> GetTasksTaskByIdAsync(int id);
        Task<List<TasksTaskDto>> GetTasksByUserIdAsync(int userId);
        Task<bool> AssignUserToTasksTaskAsync(int tasksTaskId, int userId);
        Task<bool> RemoveUserFromTask(int tasksTaskId, int userId);

    }
}
