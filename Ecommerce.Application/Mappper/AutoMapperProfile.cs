using AutoMapper;
using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Mappper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateOrUpdateOrderDTOs, Order>().ReverseMap();   
            CreateMap<GetAllOrderDTOs, Order>().ReverseMap();   
            CreateMap<CreateOrUpdateOrderItemDTOs, OrderItem>().ReverseMap();   
            CreateMap<GetAllOrderItemDTOs, OrderItem>().ReverseMap();   
        
        }

    }
}
