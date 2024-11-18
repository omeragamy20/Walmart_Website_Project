using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts
{
    public interface IOrderItemsReposatiry : IGenericReposatiry<OrderItem, int>
    {
        public void UpdatePrd(CreateOrUpdateOrderItemDTOs Dto);
        public List<GetAllOitemWithHistory> GetAllOrderItemHistory();
    }
}
