using SalesManagement.DataAccess.Context;
using SalesManagement.DataAccess.Repositories.Interfaces;
using SalesManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesManagement.DataAccess.Repositories.Implementations
{
    public class TasksTaskRepository : ITasksTaskRepository
    {
        private readonly SalesManagementDbContext _context;

        public TasksTaskRepository(SalesManagementDbContext context)
        {
            _context = context;
        }

        public async Task<TasksTask> AddAsync(TasksTask tasksTask)
        {
            await _context.TasksTasks.AddAsync(tasksTask);
            return tasksTask;
        }

        public async Task<TasksTask> GetByIdAsync(int id)
        {
            return await _context.TasksTasks.FindAsync(id);
        }

        public async Task<TasksTask> GetByIdWithUserTasksAsync(int id)
        {
            return await _context.TasksTasks
                                 .Include(tt => tt.UserTasks)
                                 .ThenInclude(ut => ut.User)
                                 .FirstOrDefaultAsync(tt => tt.Id == id);
        }

        public async Task<IEnumerable<TasksTask>> GetAllWithUserTasksAsync()
        {
            return await _context.TasksTasks
                                 .Include(tt => tt.UserTasks)
                                 .ThenInclude(ut => ut.User)
                                 .ToListAsync();
        }

        public async Task UpdateAsync(TasksTask tasksTask)
        {
            _context.TasksTasks.Update(tasksTask);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TasksTask tasksTask)
        {
            _context.TasksTasks.Remove(tasksTask);
            await Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

public async Task<List<TasksTask>> GetTasksByUserIdAsync(int userId)
{
    return await _context.TasksTasks
                         .Where(tt => tt.UserTasks.Any(ut => ut.UserId == userId)) // Kullanıcıya atanmış görevleri filtrele
                         .Include(tt => tt.UserTasks)
                         .ThenInclude(ut => ut.User) // İlişkili kullanıcı bilgilerini dahil et
                         .ToListAsync();
}
    }
}
