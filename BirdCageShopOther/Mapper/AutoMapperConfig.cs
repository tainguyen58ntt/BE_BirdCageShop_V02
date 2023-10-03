using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopViewModel.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopOther.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RoleViewModel, Role>().ReverseMap();
            CreateMap<RoleAddViewModel, Role>().ReverseMap();
            
        }   
    }
}
