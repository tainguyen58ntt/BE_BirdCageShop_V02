using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.Category;
using BirdCageShopViewModel.Feature;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ProductFeature;
using BirdCageShopViewModel.ProductImage;
using BirdCageShopViewModel.ProductReviews;
using BirdCageShopViewModel.ProductSpecifications;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.ShoppingCart;
using BirdCageShopViewModel.Specification;
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
            CreateMap<CreateFeature,Feature>().ReverseMap();
            CreateMap<GetFeature, Feature>().ReverseMap();
            CreateMap<UpdateFeature, Feature>().ReverseMap();

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
           
            CreateMap<ProductWithReviewViewModel, Product>().ReverseMap();
            CreateMap<Product, ProductViewModel>()
      .ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => src.ProductSpecifications.Select(ps => ps.Specification)))
      .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.ProductFeatures.Select(ps => ps.Feature)))
       .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages))
      .ReverseMap();
            CreateMap<Specification, SpecificationViewModel>().ReverseMap();
            CreateMap<Feature, FeatureViewModel>().ReverseMap();

            //
            //CreateMap<ProductReviewsViewModel, ProductReview>().ReverseMap();
            CreateMap<ProductReview, ProductReviewsViewModel>()
	.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
	.ReverseMap();
			/*CreateMap<ProductFeatureViewModel, ProductFeature>().ReverseMap();*/
			CreateMap<ProductSpecificationsViewModel, ProductSpecification>().ReverseMap();
			CreateMap<ProductImageViewModel, ProductImage>().ReverseMap();

			//
			CreateMap<ShoppingCart, ShoppingCartViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
				.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
				.ReverseMap();

		}
    }
}
