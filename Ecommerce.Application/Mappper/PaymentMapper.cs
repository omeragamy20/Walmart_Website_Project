using AutoMapper;
using Ecommerce.DTOs.Payment;
using Ecommerce.DTOs.Shipment;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Mappper
{
   
    public class PaymentAutoMapperProfile : Profile
    {
        public PaymentAutoMapperProfile()
        {
            CreateMap<CreateDto, Payment>().ReverseMap();
            CreateMap<Read_DTO, Payment>().ReverseMap();
        }
    }
}

