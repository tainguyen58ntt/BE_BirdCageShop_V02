using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Category;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ProductFeature;
using BirdCageShopViewModel.ProductImage;
using BirdCageShopViewModel.ProductSpecifications;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
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
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            //
            CreateMap<RoleViewModel, Role>().ReverseMap();
            CreateMap<RoleAddViewModel, Role>().ReverseMap();

            //
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<UserSignUpViewModel, User>().ReverseMap();
            CreateMap<UserChangePasswordViewModel, User>().ReverseMap();

            //
            

            //
            CreateMap<VourcherViewModel, Voucher>().ReverseMap();
            CreateMap<VourcherAddViewModel, Voucher>().ReverseMap();

            //
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<CategoryCreateViewModel, Category>().ReverseMap();


			//
			CreateMap<OrderDetailViewModel, OrderDetail>().ReverseMap();
			//
			CreateMap<OrderWithDetailViewModel, Order>().ReverseMap();

			//
			CreateMap<ProductViewModel, Product>().ReverseMap();

			//
			CreateMap<ProductFeatureViewModel, ProductFeature>().ReverseMap();
			CreateMap<ProductSpecificationsViewModel, ProductSpecification>().ReverseMap();
			CreateMap<ProductImageViewModel, ProductImage>().ReverseMap();


		}
    }
}
