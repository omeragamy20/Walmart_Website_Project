using Ecommerce.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Product_Facility
{
    public interface IProductFacilityServices
    {
        public Task<List<CreateorUpdatePrdctFaciltyDTOs>> CreatePrdFaciltyAsync(PrdFacilitySubCategory Entity);
        public Task<List<CreateorUpdatePrdctFaciltyDTOs>> UpdatePrdFaciltyAsync(PrdFacilitySubCategory Entity);
        public  Task<PrdFacilitySubCategory> GetFacilitesByPrdIdAsync(int prdID); 

    }
}
