using Ecommerce.Application.Contracts.Categories;
using Ecommerce.Context;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Categories
{
    public  class CategoryRepository:GenricReposatiry<Category,int> ,ICategoryReposatiry
    {
        public CategoryRepository(EcommerceContext context):base(context)
        {
            
        }

        
    }
}
