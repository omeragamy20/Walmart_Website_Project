using AutoMapper;
using Ecommerce.DTOs.Shipment;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Mappper
{
    //public  class AutoMapperProfile:Profile
    //{
    //    CreateMap<CreateDto, Shipment>().ReverseMap();
    //    CreateMap<GetAllDto, Shipment>().ReverseMap();
    //}

    public class AutoMapperProfile : Profile
    {
    public AutoMapperProfile()
    {
        CreateMap<CreateDto, Shipment>().ReverseMap();
        CreateMap<GetAllDto, Shipment>().ReverseMap();
    }
}
}
