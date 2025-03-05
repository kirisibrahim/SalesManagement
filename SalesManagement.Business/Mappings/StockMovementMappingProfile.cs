using AutoMapper;
using SalesManagement.Business.DTOs;
using SalesManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement.Business.Mappings
{
    public class StockMovementMappingProfile : Profile
    {
        public StockMovementMappingProfile()
        {
            CreateMap<StockMovement, StockMovementDto>().ReverseMap();
        }
    }
}
