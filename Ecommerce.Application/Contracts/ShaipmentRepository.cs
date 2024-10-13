using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts
{
    public interface IShipmentRepo : IGenericReposatiry<Shipment, int>
    {
        IQueryable<Shipment> SearchOption(string keyword); 
    }
}
