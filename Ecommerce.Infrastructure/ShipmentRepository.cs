using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Ecommerce.Infrastructure
{
    public class ShaipmentRepository : GenricReposatiry<Shipment, int>, IShaipmentRepository
    {
      
        public ShaipmentRepository(EcommerceContext ecommerce):base(ecommerce)
        {
                
        }

        public IQueryable<Shipment> SearchOption(string keyword)
        {
            throw new NotImplementedException();
        }
    }

}