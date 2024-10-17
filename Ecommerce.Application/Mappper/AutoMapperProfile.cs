using AutoMapper;
using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.DTOs.Facility;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.Shipment;
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
            CreateMap<Product,CreateAndUpdateProductDTO>().ReverseMap();
            CreateMap<GetAllproductEnDTO, Product>().ReverseMap();
            CreateMap<GetAllProductArDTO, Product>().ReverseMap();
            CreateMap<CreateDto, Shipment>().ReverseMap();
            CreateMap<GetAllDto, Shipment>().ReverseMap();
            CreateMap<Images, ImageDTO>().ReverseMap();
            CreateMap<Product, GetAllproductDTO>()
            .ForMember(dest => dest.SubCategoryIds, opt => opt.MapFrom(src => src.productSubCategory
            .Select(ps => ps.SubcategoryId ?? 0).ToList()))
            .ForMember(dest => dest.SubCategoryNames, opt => opt.
            MapFrom(src => src.productSubCategory.Select(ps => ps.SubCategory.Name_en).ToList()))
            .ForMember(dest => dest.SubCategoryNamesAr, opt => opt.MapFrom(src => src.productSubCategory.Select(sf => sf.SubCategory.Name_ar).ToList())).ReverseMap();
            CreateMap<Facility, FacilityDTO>().ForMember(dest=>dest.SubCategoryIds,opt=>opt.MapFrom(src=>src.subCatFacility
            .Select(sf=>sf.SubCategoryID).ToList()))
            .ForMember(dest=>dest.SubCategoryNames,opt=>opt.MapFrom(src=>src.subCatFacility.Select(sf=>sf.subCategory.Name_en)
            .ToList())).ForMember(dest => dest.SubCategoryNamesAr, opt => opt.MapFrom(src => src.subCatFacility.Select(sf => sf.subCategory.Name_ar).ToList())).ReverseMap();

            //CreateMap<CreatePaymentDTO, Payment>().ReverseMap();
            //CreateMap<GetAllPaymentDTO, Payment>().ReverseMap();
        }
    }
}

