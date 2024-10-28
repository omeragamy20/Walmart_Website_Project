using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class ProductRepository : GenricReposatiry<Product, int>, IProductRepository
    {
        private readonly EcommerceContext Context;

        public ProductRepository(EcommerceContext _Context) : base(_Context)
        {
            Context = _Context;
        }
        public async Task<List<Product>> GetPrdBySubCat(int id)
        {
           return await Context.ProductSubCategories.Where(p=>p.Id == id).
                Select(p=>p.Product).ToListAsync();
        }
    }
}
