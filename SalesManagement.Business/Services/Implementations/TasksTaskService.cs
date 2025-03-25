using AutoMapper;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Services.Interfaces;
using SalesManagement.DataAccess.Repositories.Interfaces;
using SalesManagement.Entities.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SalesManagement.Business.Services.Implementations
{
    public class TasksTaskService : ITasksTaskService
    {
        private readonly ITasksTaskRepository _repository;
        private readonly IMapper _mapper;

        public TasksTaskService(ITasksTaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TasksTaskDto> CreateTasksTaskAsync(TasksTaskDto tasksTaskDto)
        {
            var entity = _mapper.Map<TasksTask>(tasksTaskDto);
            entity.CreatedDate = System.DateTime.Now;
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            foreach (var userId in tasksTaskDto.UserIds)
            {
                if (!entity.UserTasks.Any(ut => ut.UserId == userId))
                {
                    entity.UserTasks.Add(new UserTask { TaskId = entity.Id, UserId = userId });
                }
            }

            await _repository.SaveChangesAsync();
            return _mapper.Map<TasksTaskDto>(entity);
        }

        public async Task<bool> UpdateTasksTaskAsync(int id, UpdateTasksTaskDto updateDto)
        {
            var entity = await _repository.GetByIdWithUserTasksAsync(id);
            if (entity == null)
                return false;

            entity.Title = updateDto.Title;
            entity.Description = updateDto.Description;
            entity.Durum = updateDto.Durum;

            foreach (var userId in updateDto.UserIds)
            {
                if (!entity.UserTasks.Any(ut => ut.UserId == userId))
                {
                    entity.UserTasks.Add(new UserTask { TaskId = id, UserId = userId });
                }
            }

            await _repository.UpdateAsync(entity);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteTasksTaskAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity);
            return await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TasksTaskDto>> GetAllTasksTasksAsync()
        {
            var list = await _repository.GetAllWithUserTasksAsync();
            return _mapper.Map<IEnumerable<TasksTaskDto>>(list);
        }

        public async Task<TasksTaskDto> GetTasksTaskByIdAsync(int id)
        {
            var entity = await _repository.GetByIdWithUserTasksAsync(id);
            return _mapper.Map<TasksTaskDto>(entity);
        }

        public async Task<bool> AssignUserToTasksTaskAsync(int tasksTaskId, int userId)
        {
            var entity = await _repository.GetByIdWithUserTasksAsync(tasksTaskId);
            if (entity == null)
                return false;

            if (entity.UserTasks.Any(ut => ut.UserId == userId))
                return false;

            entity.UserTasks.Add(new UserTask { TaskId = tasksTaskId, UserId = userId });
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> RemoveUserFromTask(int tasksTaskId, int userId)
        {
            // Görevi ve ilişkili kullanıcıları alıyoruz
            var entity = await _repository.GetByIdWithUserTasksAsync(tasksTaskId);
            if (entity == null)
            {
                return false;
            }

            // Kullanıcı görevde mevcut mu kontrol ediyoruz
            var userTask = entity.UserTasks.FirstOrDefault(ut => ut.UserId == userId);
            if (userTask == null)
            {
                return false;
            }

            // Kullanıcıyı görevden kaldırıyoruz
            entity.UserTasks.Remove(userTask);

            // Değişiklikleri kaydediyoruz
            return await _repository.SaveChangesAsync();
        }

        public async Task<List<TasksTaskDto>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = await _repository.GetTasksByUserIdAsync(userId);
            return _mapper.Map<List<TasksTaskDto>>(tasks);
        }
    }
}
