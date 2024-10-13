using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class FacilityRepository : GenricReposatiry<Facility, int>, IFacilityRepository
    {
        public FacilityRepository(EcommerceContext _Context) : base(_Context)
        {
        }
    }
}
