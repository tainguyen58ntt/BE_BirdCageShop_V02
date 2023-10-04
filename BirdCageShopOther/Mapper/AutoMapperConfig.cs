using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
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

            //
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<UserSignUpViewModel, User>().ReverseMap();

            //
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));


        }
    }
}
