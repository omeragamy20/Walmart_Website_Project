using AutoMapper;
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
    public class ShipmentRepository : GenricReposatiry<Shipment, int>, IShaipmentRepository
    {
        private readonly EcommerceContext _ecommerce;

        public ShipmentRepository(EcommerceContext ecommerce) : base(ecommerce)
        {
            _ecommerce = ecommerce;
        }

        public IQueryable<Shipment> SearchOption(string keyword)
        {
            throw new NotImplementedException();
        }


        public async Task<Shipment> GetOneByZipCodeAsync(string zipCode)
        {
            return await _ecommerce.Shipments
                .FirstOrDefaultAsync(s => s.ZipCode == zipCode);
        }


    }
}

