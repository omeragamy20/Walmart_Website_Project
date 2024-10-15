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
            CreateMap<CreateAndUpdateProductDTO, Product>().ReverseMap();
            CreateMap<GetAllproductDTO, Product>().ReverseMap();
            CreateMap<GetAllproductEnDTO, Product>().ReverseMap();
            CreateMap<GetAllProductArDTO, Product>().ReverseMap();
            CreateMap<FacilityDTO, Facility>().ReverseMap();
            CreateMap<CreateDto, Shipment>().ReverseMap();
            CreateMap<GetAllDto, Shipment>().ReverseMap();
            //CreateMap<CreatePaymentDTO, Payment>().ReverseMap();
            //CreateMap<GetAllPaymentDTO, Payment>().ReverseMap();
        }
    }
}

