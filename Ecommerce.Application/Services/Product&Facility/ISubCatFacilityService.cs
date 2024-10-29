using Ecommerce.DTOs.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Product_Facility
{
    public interface ISubCatFacilityService
    {
        public Task<List<CreatorUpdateFacilityDTO>> Getallfacilitybyubcatid(List<int>? subcatids);
    }
}
