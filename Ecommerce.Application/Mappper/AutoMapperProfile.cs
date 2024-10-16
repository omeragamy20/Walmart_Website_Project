using AutoMapper;
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
            CreateMap<CreateorUpdatedCategoryDTOs,Category>().ReverseMap();
            CreateMap<GetAllCategoryDTOs,Category>().ReverseMap();
            CreateMap<CreateorUpdatedSubCategoryDTOs,SubCategory>().ReverseMap();
            CreateMap<GetAllSubCategoryDTOs, SubCategory>().ReverseMap();
            CreateMap<Product,CreateAndUpdateProductDTO>()
           .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(img => img.Image).ToList()))
           .ReverseMap();
            CreateMap<GetAllproductEnDTO, Product>().ReverseMap();
            CreateMap<GetAllProductArDTO, Product>().ReverseMap();
            CreateMap<FacilityDTO, Facility>().ReverseMap();
            CreateMap<CreateDto, Shipment>().ReverseMap();
            CreateMap<GetAllDto, Shipment>().ReverseMap();
            CreateMap<Images, ImageDTO>().ReverseMap();
            CreateMap<Product, GetAllproductDTO>()
            .ForMember(dest => dest.SubCategoryIds, opt => opt.MapFrom(src => src.productSubCategory.Select(ps => ps.SubcategoryId ?? 0).ToList()))
            .ForMember(dest => dest.SubCategoryNames, opt => opt.
            MapFrom(src => src.productSubCategory.Select(ps => ps.SubCategory.Name_en).ToList())).ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.Image).ToList())).ReverseMap();
            //CreateMap<GetAllproductDTO, Product>().ReverseMap();
            CreateMap<CreateorUpdateFavoritDTO,Favorite>().ReverseMap();
            CreateMap<GetAllFavoritDTO,Favorite>().ReverseMap();
            CreateMap<CreateorUpdateRateDTO, Rate>().ReverseMap();
            CreateMap<GetAllRateDTO,Rate>().ReverseMap();
            //CreateMap<CreatePaymentDTO, Payment>().ReverseMap();
            //CreateMap<GetAllPaymentDTO, Payment>().ReverseMap();
            CreateMap< CreateAdminDto , Customer>().ReverseMap();
            CreateMap< GetAllUsersDto , Customer>().ReverseMap();
             CreateMap<GetAdminDto , Customer>().ReverseMap();
             CreateMap<UpdataAdminDto , Customer>().ReverseMap();
        }
    }
}

