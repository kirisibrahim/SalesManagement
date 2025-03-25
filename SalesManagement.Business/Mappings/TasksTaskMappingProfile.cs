// SalesManagement.Business/Mappings/TasksTaskMappingProfile.cs
using AutoMapper;
using SalesManagement.Entities.Models;
using SalesManagement.Business.DTOs;
using System.Linq;

namespace SalesManagement.Business.Mappings
{
    public class TasksTaskMappingProfile : Profile
    {
        public TasksTaskMappingProfile()
        {
            CreateMap<TasksTask, TasksTaskDto>()
                .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.UserTasks.Select(ut => ut.UserId)));

            CreateMap<TasksTaskDto, TasksTask>()
                .ForMember(dest => dest.UserTasks, opt => opt.MapFrom(src => src.UserIds.Select(id => new UserTask { UserId = id })));
        }
    }
}
