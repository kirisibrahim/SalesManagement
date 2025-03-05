using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SalesManagement.Business.DTOs;
using SalesManagement.Entities.Models;

namespace SalesManagement.Business.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Role, opt => opt.Ignore()) // RoleId ile erişim sağlıycaz ondan Role yi atladıkk
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId)); // RoleId'yi User a eşledik
        }
    }

}
