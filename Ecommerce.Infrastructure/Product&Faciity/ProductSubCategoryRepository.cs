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
    public class ProductSubCategoryRepository:GenricReposatiry<ProductSubCategory,int>,IProductSubCategoryRepository
    {
        public ProductSubCategoryRepository(EcommerceContext context):base(context)
        {
            
        }
    }
}
