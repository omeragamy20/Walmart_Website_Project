using Ecommerce.Application.Contracts.product_Facillity;
using Ecommerce.Context;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Product_Faciity
{
    public class SubCatFacilityRepository:GenricReposatiry<subCatFacility,int>,ISubCatFacilityRepository
    {
        public SubCatFacilityRepository(EcommerceContext context):base(context)
        {
            
        }
    }
}
