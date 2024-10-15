using AutoMapper;
using Ecommerce.DTOs.CustomerDto;
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
        public AutoMapperProfile() {
            CreateMap< CreateAdminDto , Customer>().ReverseMap();
            CreateMap< GetAllUsersDto , Customer>().ReverseMap();
             CreateMap<GetAdminDto , Customer>().ReverseMap();
             CreateMap<UpdataAdminDto , Customer>().ReverseMap();
        }
    }
}
