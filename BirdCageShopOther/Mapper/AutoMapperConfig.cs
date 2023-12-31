﻿using AutoMapper;
using BirdCageShopDbContext.Models;
using BirdCageShopDomain.Models;
using BirdCageShopUtils.Pagination;
using BirdCageShopViewModel.BirdCageType;
using BirdCageShopViewModel.Category;
using BirdCageShopViewModel.Design;
using BirdCageShopViewModel.Feature;
using BirdCageShopViewModel.Formula;
using BirdCageShopViewModel.Order;
using BirdCageShopViewModel.Product;
using BirdCageShopViewModel.ProductFeature;
using BirdCageShopViewModel.ProductImage;
using BirdCageShopViewModel.ProductReviews;
using BirdCageShopViewModel.ProductSpecifications;
using BirdCageShopViewModel.Role;
using BirdCageShopViewModel.ShoppingCart;
//using BirdCageShopViewModel.ShoppingCart;
using BirdCageShopViewModel.Specification;
using BirdCageShopViewModel.Status;
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
            CreateMap<DesignViewModel, Design>().ReverseMap();
            CreateMap<CreateDesign, Design>().ReverseMap();

            //
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
            CreateMap<UserSignUpViewModel, ApplicationUser>().ReverseMap();
            CreateMap<UserChangePasswordViewModel, ApplicationUser>().ReverseMap();

            //
            CreateMap<Formula, FormulaViewModel>()
                .ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => src.FormulaSpecifications.Select(ps => ps.Specification)))
                .ReverseMap();
            CreateMap<CreateFormulaViewModel, Formula>().ReverseMap();

            CreateMap<UpdateFormulaViewModel, Formula>().ReverseMap();
            //
            CreateMap<CreateBirdCageType, BirdCageType>().ReverseMap();
            CreateMap<GetBirdCageType, BirdCageType>().ReverseMap();
            CreateMap<UpdateBirdCageType, BirdCageType>().ReverseMap();
            //
            CreateMap<VourcherViewModel, Voucher>().ReverseMap();
            CreateMap<VourcherAddViewModel, Voucher>().ReverseMap();

            //
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<CategoryCreateViewModel, Category>().ReverseMap();
            //
            CreateMap<CreateSpecification, Specification>().ReverseMap();
            CreateMap<GetSpecification, Specification>().ReverseMap();
            CreateMap<UpdateSpecification, Specification>().ReverseMap();
            //
            CreateMap<CreateFeature, Feature>().ReverseMap();
            CreateMap<GetFeature, Feature>().ReverseMap();
            CreateMap<UpdateFeature, Feature>().ReverseMap();
            //
            CreateMap<OrderDetail, OrderDetailViewModel>()
                .ForMember(dest => dest.ProductTitle, opt => opt.MapFrom(src => src.Product.Title))
                 .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.Product.ProductImages))
                .ReverseMap();
            //
            CreateMap<Order, OrderWithDetailViewModel>()
     .ForMember(dest => dest.NameRecieved, opt => opt.MapFrom(src => src.NameRecieved))
     .ReverseMap();


            //
            CreateMap<ProductViewModel, Product>().ReverseMap();

            CreateMap<ProductWithReviewViewModel, Product>().ReverseMap();
            CreateMap<Product, ProductWithReviewViewModel>().ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => src.ProductSpecifications.Select(ps => ps.Specification)))
              .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.ProductFeatures.Select(ps => ps.Feature)))
              .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages)).ReverseMap();
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
    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
    .ReverseMap();
            CreateMap<ProductFeatureViewModel, ProductFeature>().ReverseMap();
            CreateMap<ProductSpecificationsViewModel, ProductSpecification>().ReverseMap();
            CreateMap<ProductImageViewModel, ProductImage>().ReverseMap();
            //
            CreateMap<ShoppingCart, ShoppingCartViewModel>()
                .ForMember(dest => dest.ProductViewModel, opt => opt.MapFrom(src => src.Product))

                .ReverseMap();

            ///

            CreateMap<Status, StatusViewModel>().ReverseMap();


        }
    }
}
