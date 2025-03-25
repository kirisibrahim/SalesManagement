using SalesManagement.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesManagement.DataAccess.Repositories.Interfaces
{
    public interface ITasksTaskRepository
    {
        Task<TasksTask> AddAsync(TasksTask tasksTask);
        Task<TasksTask> GetByIdAsync(int id);
        Task<TasksTask> GetByIdWithUserTasksAsync(int id);
        Task<List<TasksTask>> GetTasksByUserIdAsync(int userId);
        Task<IEnumerable<TasksTask>> GetAllWithUserTasksAsync();
        Task UpdateAsync(TasksTask tasksTask);
        Task DeleteAsync(TasksTask tasksTask);
        Task<bool> SaveChangesAsync();

    }
}
