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
    public class ImageRepository : GenricReposatiry<Images, int>, IImageRepository
    {
        private readonly EcommerceContext Context;

        public ImageRepository(EcommerceContext _Context) : base(_Context)
        {
            Context = _Context;
        }
    }
}
