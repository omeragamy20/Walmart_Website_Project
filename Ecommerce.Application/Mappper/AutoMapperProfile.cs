﻿using AutoMapper;
using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.Models;
using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.DTOs.DTOsFavoritandRate;
using Ecommerce.DTOs.Facility;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.Shipment;
using Ecommerce.DTOs.CustomerDto;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Order & OrderItem
            CreateMap<CreateOrUpdateOrderDTOs, Order>().ReverseMap();   
            CreateMap<GetAllOrderDTOs, Order>().ReverseMap();   
            CreateMap<CreateOrUpdateOrderItemDTOs, OrderItem>().ReverseMap();   
            CreateMap<GetAllOrderItemDTOs, OrderItem>().ReverseMap();   
           
            //categoryand subcategory
            CreateMap<CreateorUpdatedCategoryDTOs,Category>().ReverseMap();
             CreateMap<CreateorUpdatedCategoryDTOs,Category>().ReverseMap();
            CreateMap<GetAllCategoryDTOs,Category>().ReverseMap();
            CreateMap<CreateorUpdatedSubCategoryDTOs,SubCategory>().ReverseMap();
            CreateMap<GetAllSubCategoryDTOs, SubCategory>().ReverseMap();
            
            // product & Facility & Image
            CreateMap<Product,CreateAndUpdateProductDTO>().ReverseMap();
            //  .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(img => img.Image).ToList()))
            CreateMap<Product, GetAllproductDTO>()
            .ForMember(dest => dest.SubCategoryIds, opt => opt.MapFrom(src => src.productSubCategory.Select(ps => ps.SubcategoryId ?? 0).ToList()))
            .ForMember(dest => dest.SubCategoryNames, opt => opt.
            MapFrom(src => src.productSubCategory.Select(ps => ps.SubCategory.Name_en).ToList())).ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.Image).ToList())).ReverseMap();
            CreateMap<GetAllproductEnDTO, Product>().ReverseMap();
            CreateMap<GetAllProductArDTO, Product>().ReverseMap();
            CreateMap<FacilityDTO, Facility>().ReverseMap();
            CreateMap<Images, ImageDTO>().ReverseMap();
            
            // favorit & Rate
            CreateMap<CreateorUpdateFavoritDTO,Favorite>().ReverseMap();
            CreateMap<GetAllFavoritDTO,Favorite>().ReverseMap();
            CreateMap<CreateorUpdateRateDTO, Rate>().ReverseMap();
            CreateMap<GetAllRateDTO,Rate>().ReverseMap();

            // Customer
            CreateMap< CreateAdminDto , Customer>().ReverseMap();
            CreateMap< GetAllUsersDto , Customer>().ReverseMap();
            CreateMap<GetAdminDto , Customer>().ReverseMap();
            CreateMap<UpdataAdminDto , Customer>().ReverseMap();
            // mady work
            CreateMap<CreateDto, Shipment>().ReverseMap();
            CreateMap<GetAllDto, Shipment>().ReverseMap();
            //CreateMap<GetAllproductDTO, Product>().ReverseMap();
            //CreateMap<CreatePaymentDTO, Payment>().ReverseMap();
            //CreateMap<GetAllPaymentDTO, Payment>().ReverseMap();
        }

    }
}


