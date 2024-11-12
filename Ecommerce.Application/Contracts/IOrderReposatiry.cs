using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts
{
    public interface IOrderReposatiry :IGenericReposatiry<Order,int>
    {
        public List<GetCustomerOrders> GetAllByCusIdAsync(string id);
    }
}
