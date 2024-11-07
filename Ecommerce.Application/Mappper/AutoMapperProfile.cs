using AutoMapper;
using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.Models;
using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.DTOs.DTOsFavoritandRate;
using Ecommerce.DTOs.Facility;
using Ecommerce.DTOs.Payment;
using Ecommerce.DTOs.Product;
using Ecommerce.DTOs.Shipment;
using Ecommerce.DTOs.CustomerDto;
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
            // Category and subcategory
            CreateMap<CreateorUpdatedCategoryDTOs, Category>().ReverseMap();
            CreateMap<GetAllCategoryDTOs, Category>().ReverseMap();
            CreateMap<CreateorUpdatedSubCategoryDTOs, SubCategory>().ReverseMap();
            CreateMap<GetAllSubCategoryDTOs, SubCategory>().ReverseMap();

            //Order & OrderItem
            CreateMap<CreateOrUpdateOrderDTOs, Order>().ReverseMap();   
            CreateMap<GetAllOrderDTOs, Order>().ReverseMap();   
            CreateMap<CreateOrUpdateOrderItemDTOs, OrderItem>().ReverseMap();   
            CreateMap<GetAllOrderItemDTOs, OrderItem>().ReverseMap();
            //   CreateMap<GetAllProductArDTO, OrderItem>()
            //   .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new Product
            //   {
            //    Id = src.Id,
            //    Price = src.Price,
            //// Map other properties as needed
            //   }));

            //   CreateMap<OrderItem, GetAllProductArDTO>()
            //  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Product.Id))
            //  .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));



            //product & Facility & Image
            CreateMap<Product, CreateAndUpdateProductDTO>().ReverseMap();
            ////  .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(img => img.Image).ToList()))
            //CreateMap<Product, GetAllproductDTO>()
            //.ForMember(dest => dest.SubCategoryIds, opt => opt.MapFrom(src => src.productSubCategory
            //.Select(ps => ps.SubcategoryId ?? 0).ToList()))
            //.ForMember(dest => dest.SubCategoryNames, opt => opt.
            // MapFrom(src => src.productSubCategory.Select(ps => ps.SubCategory.Name_en).ToList()))
            //.ForMember(dest => dest.SubCategoryNamesAr, opt => opt.MapFrom(src => src.productSubCategory.Select(sf => sf.SubCategory.Name_ar).ToList()))
            //.ForMember(dest => dest.Facilities, opt => opt.MapFrom(src => src.ProductFacilities.Select(pf=>pf.Value_en).ToList()))
            ////.ForMember(dest => dest.Facilities, opt => opt.MapFrom(src => src.productSubCategory
            ////.Select(ps => ps.SubCategory.subCatFacility.Select(f => f.facility.Name_en).ToList())))
            //.ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.Image).ToList()))
            //.ReverseMap();
            CreateMap<GetAllproductDTO, Product>().ReverseMap();
            CreateMap<GetAllproductEnDTO, Product>().ReverseMap();
            CreateMap<GetAllProductArDTO, Product>().ReverseMap();

            // Facilitiy
            CreateMap<CreatorUpdateFacilityDTO, Facility>().ReverseMap();
            CreateMap< FacilityDTO, Facility >().ReverseMap();
            CreateMap<Images, ImageDTO>().ReverseMap();
            //     //CreateMap<GetAllproductDTO, Product>().ReverseMap();
            //MapFrom(src => src.productSubCategory.Select(ps => ps.SubCategory.Name_en).ToList()))
            //.ForMember(dest => dest.SubCategoryNamesAr, opt => opt.MapFrom(src => src.productSubCategory.Select(sf => sf.SubCategory.Name_ar).ToList())).ReverseMap();
            //CreateMap<Facility, FacilityDTO>().ForMember(dest=>dest.SubCategoryIds,opt=>opt.MapFrom(src=>src.subCatFacility
            //.Select(sf=>sf.SubCategoryID).ToList()))
            //.ForMember(dest=>dest.SubCategoryNames,opt=>opt.MapFrom(src=>src.subCatFacility.Select(sf=>sf.subCategory.Name_en)
            //.ToList())).ForMember(dest => dest.SubCategoryNamesAr, opt => opt.MapFrom(src => src.subCatFacility.Select(sf => sf.subCategory.Name_ar).ToList())).ReverseMap();
            CreateMap<CreateorUpdatePrdctFaciltyDTOs, ProductFacility>().ReverseMap();

            // favorit & Rate
            CreateMap<CreateorUpdateFavoritDTO,Favorite>().ReverseMap();
            CreateMap<CreateFavoriteDTOs, Favorite>().ReverseMap();
            CreateMap<GetAllFavoritDTO,Favorite>().ReverseMap();
            CreateMap<CreateorUpdateRateDTO, Rate>().ReverseMap();
            CreateMap<GetAllRateDTO,Rate>().ReverseMap();

            // Customer
            CreateMap< CreateAdminDto , Customer>().ReverseMap();
            CreateMap< CreateCustomerDto , Customer>().ReverseMap();
            CreateMap< GetAllUsersDto , Customer>().ReverseMap();
            CreateMap<GetAdminDto , Customer>().ReverseMap();
            CreateMap<GetUserDto , Customer>().ReverseMap();
            CreateMap<UpdataAdminDto , Customer>().ReverseMap();
            
            // mady work
            CreateMap<GetAllDto, Shipment>().ReverseMap();
            CreateMap<CreateDto, Shipment>().ReverseMap();
            CreateMap<UpdateDTO, Shipment>().ReverseMap();
            CreateMap<GetAllDtos, Payment>().ReverseMap();
            CreateMap<CreateDtos, Payment>().ReverseMap();
            CreateMap<UpdateDTOs, Payment>().ReverseMap();
        }

    }
}


