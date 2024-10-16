using Ecommerce.Application.Contracts.FavortandRateRepo;
using Ecommerce.Context;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.FavortandRate
{
    public class RateRepository:GenricReposatiry<Rate,int>, IRateRepository
    {
        public RateRepository(EcommerceContext context) : base(context)
        {
            
        }
    }
}
