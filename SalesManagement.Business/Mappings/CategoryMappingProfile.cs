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
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
