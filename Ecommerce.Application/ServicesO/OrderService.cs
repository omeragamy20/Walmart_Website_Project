using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.ServicesO
{
    public class OrderService : IOrderService
    {
        private readonly IOrderReposatiry OrderRepo;
        private readonly IMapper Maper;

        public OrderService(IOrderReposatiry _OrderRepo,IMapper _Maper)
        {
            OrderRepo = _OrderRepo;
            Maper = _Maper;

        }

        public async Task<ResultView<CreateOrUpdateOrderDTOs>> CreateAsync(CreateOrUpdateOrderDTOs entity)
        {
           
            ResultView<CreateOrUpdateOrderDTOs> result = new();
            try
            {


                bool Exist = (await OrderRepo.GetAllAsync()).Any(p => p.Id == entity.Id && p.OrderDate == entity.OrderDate);

                if (Exist)
                {
                    result = new()
                    {
                        Entity = null,
                       IsSuccess = false,
                       Message = "The Order Already Exist"
                    };

                    return result;

                }

                var prd = Maper.Map<Order>(entity);
                var SucessEntity = await OrderRepo.CreateAsync(prd);
                await OrderRepo.SaveChanges();
                var prdchange = Maper.Map<CreateOrUpdateOrderDTOs>(SucessEntity);

                result = new()
                {
                    Entity = prdchange,
                    IsSuccess = true,
                    Message = "Created Sucess"
                };

                return result;

            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Create is Feild" + ex.Message
                };
                return result;

            }
        }

        public async Task<GetAllOrderDTOs> DeleteAsync(int Id)
        {
           var thisorder = await OrderRepo.GetOneAsync(Id);
            var delete = (await OrderRepo.DeleteAsync(thisorder));
           await OrderRepo.SaveChanges();
            return Maper.Map<GetAllOrderDTOs>(delete);
        }

        public async Task<List<GetAllOrderDTOs>> GetAllAsync()
        {
            var all = (await OrderRepo.GetAllAsync()).ToList();

            return Maper.Map<List<GetAllOrderDTOs>>(all);
        }



        public async Task<ResultView<GetAllOrderDTOs>> GetOneAsync(int Id)
        {
            var one = await OrderRepo.GetOneAsync(Id);
            
           var onemapping = Maper.Map<GetAllOrderDTOs>(one);
            ResultView<GetAllOrderDTOs> res = new()
            {
                Entity = onemapping,
                IsSuccess = true,
                Message = "Success!"

            };


            return res;


        }



        public async Task<ResultView<CreateOrUpdateOrderDTOs>> UpdateAsync(CreateOrUpdateOrderDTOs entity)
        {

            ResultView<CreateOrUpdateOrderDTOs> result = new();
            try
            {



                var ex = (await OrderRepo.GetOneAsync(entity.Id));


                if (ex != null)
                {

                    var prd = Maper.Map<Order>(entity);
                    var SucessEntity = await OrderRepo.UpdateAsync(prd);
                    await OrderRepo.SaveChanges();
                    var prdchange = Maper.Map<CreateOrUpdateOrderDTOs>(SucessEntity);

                    result = new()
                    {
                        Entity = prdchange,
                        IsSuccess = true,
                        Message = "Created Sucess"
                    };

                    return result;
                    

                }
                result = new()
                {

                    Entity = null,
                    IsSuccess = false,
                    Message = "Dosent Found"
                };

                return result;



            }
            catch (Exception ex)
            {
                result = new()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Update is Feild" + ex.Message
                };
                return result;

            }


        }
    }
}
