using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.ServicesO
{
    public interface IOrderService
    {
        //public Task<ResultView<CreateOrUpdateOrderDTOs>> CreateAsync(CreateOrUpdateOrderDTOs entity);
        public Task<CreateOrUpdateOrderDTOs> CreateAsync(CreateOrUpdateOrderDTOs entity);

        public Task<ResultView<CreateOrUpdateOrderDTOs>> UpdateAsync(CreateOrUpdateOrderDTOs entity);
        public Task<ResultView<GetAllOrderDTOs>> GetOneAsync(int Id);

        public  Task<Order> GetOneOrderAsync(int Id);
        public Task<EntityPaginated<GetAllOrderDTOs>> GetAllAsyncPagination(int PageNumber, int Count);
        public Task<List<GetAllOrderDTOs>> GetAllAsync();

        public Task<GetAllOrderDTOs> DeleteAsync(int Id);

        public List<GetCustomerOrders> GetOrdersByCusId(string id);

        //public Task<List<GetAllBookDTOs>> GetAllWithDeleteAsync();
        //public Task<EntityPagienated<GetAllBookDTOs>> GetAllPaginatedAsync(int Count, int PageNumber);

    }
}
