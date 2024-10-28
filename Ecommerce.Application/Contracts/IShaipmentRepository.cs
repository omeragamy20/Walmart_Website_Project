using AutoMapper;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts
{
    public interface IShaipmentRepository : IGenericReposatiry<Shipment, int>
    {

     
       public  Task<Shipment> GetOneByZipCodeAsync(string zipCode);

      public IQueryable<Shipment> SearchOption(string keyword); 
    }
    
}
