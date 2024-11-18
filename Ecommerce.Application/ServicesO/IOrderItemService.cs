using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.DTOs.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.ServicesO
{
    public interface IOrderItemService
    {
        public Task<ResultView<CreateOrUpdateOrderItemDTOs>> CreateAsync(CreateOrUpdateOrderItemDTOs entity);

        public Task<ResultView<CreateOrUpdateOrderItemDTOs>> UpdateAsync(CreateOrUpdateOrderItemDTOs entity);
        public Task<ResultView<GetAllOrderItemDTOs>> GetOneAsync(int Id);


        public Task<List<GetAllOrderItemDTOs>> GetAllAsync();

        public Task<GetAllOrderItemDTOs> DeleteAsync(int Id);
        public Task<List<GetAllOrderItemDTOs>> GetAllItemsAsync(int Id);
        public  Task<List<GetAllOrderItemDTOs>> GetAllWithPrdAsync();
        public List<GetAllOitemWithHistory> GetAllOrderItemHistory();
        //public Task<List<GetAllOrderItemDTOs>> SearchByName(string Search);
    }
}
