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

        // 1. Yeni Görev Oluşturma
        public async Task<TasksTaskDto> CreateTasksTaskAsync(TasksTaskDto tasksTaskDto)
        {
            var entity = _mapper.Map<TasksTask>(tasksTaskDto);
            entity.CreatedDate = System.DateTime.Now;
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            // Kullanıcıları ekleme
            foreach (var userId in tasksTaskDto.UserIds)
            {
                if (!entity.UserTasks.Any(ut => ut.UserId == userId))
                {
                    entity.UserTasks.Add(new UserTask { TaskId = entity.Id, UserId = userId });
                }
            }

            await _repository.SaveChangesAsync(); // Kullanıcıları ekledikten sonra kaydet
            return _mapper.Map<TasksTaskDto>(entity);
        }

        // 2. Görev Güncelleme
        public async Task<bool> UpdateTasksTaskAsync(int id, UpdateTasksTaskDto updateDto)
        {
            var entity = await _repository.GetByIdWithUserTasksAsync(id);
            if (entity == null)
                return false;

            entity.Title = updateDto.Title;
            entity.Description = updateDto.Description;
            entity.Durum = updateDto.Durum;

            // Kullanıcıları ekleme
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

        // 3. Görev Silme
        public async Task<bool> DeleteTasksTaskAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity);
            return await _repository.SaveChangesAsync();
        }

        // 4. Görevleri Listeleme
        public async Task<IEnumerable<TasksTaskDto>> GetAllTasksTasksAsync()
        {
            var list = await _repository.GetAllWithUserTasksAsync();
            return _mapper.Map<IEnumerable<TasksTaskDto>>(list);
        }

        // 5. Görevi ID ile Getirme
        public async Task<TasksTaskDto> GetTasksTaskByIdAsync(int id)
        {
            var entity = await _repository.GetByIdWithUserTasksAsync(id);
            return _mapper.Map<TasksTaskDto>(entity);
        }

        // 6. Kullanıcı Atama
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
                // Eğer görev bulunamazsa, false döner
                return false;
            }

            // Kullanıcı görevde mevcut mu kontrol ediyoruz
            var userTask = entity.UserTasks.FirstOrDefault(ut => ut.UserId == userId);
            if (userTask == null)
            {
                // Eğer kullanıcı görevde yoksa, işlem yapma
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
