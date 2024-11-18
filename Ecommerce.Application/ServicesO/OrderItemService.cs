using AutoMapper;
using Ecommerce.Application.Contracts;
using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.ServicesO
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemsReposatiry OrderitemRepo;
        private readonly IMapper Maper;

        public OrderItemService(IOrderItemsReposatiry _OrderitemRepo, IMapper _Maper)
        {
            OrderitemRepo = _OrderitemRepo;
            Maper = _Maper;
        }

        public async Task<ResultView<CreateOrUpdateOrderItemDTOs>> CreateAsync(CreateOrUpdateOrderItemDTOs entity)
        {
            ResultView<CreateOrUpdateOrderItemDTOs> result = new();
            try
            {


                bool Exist = (await OrderitemRepo.GetAllAsync()).Any(p => p.ProductId == entity.ProductId&&p.OrderId==entity.OrderId);

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
                OrderitemRepo.UpdatePrd(entity);
                var prd =  Maper.Map<OrderItem>(entity);
                var SucessEntity = await OrderitemRepo.CreateAsync(prd);
                await OrderitemRepo.SaveChanges();
                var prdchange = Maper.Map<CreateOrUpdateOrderItemDTOs>(SucessEntity);

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

        public async Task<GetAllOrderItemDTOs> DeleteAsync(int Id)
        {
            var thisorder = await OrderitemRepo.GetOneAsync(Id);
            var delete = (await OrderitemRepo.DeleteAsync(thisorder));
            await OrderitemRepo.SaveChanges();
            return Maper.Map<GetAllOrderItemDTOs>(delete);
        }

        public async Task<List<GetAllOrderItemDTOs>> GetAllAsync()
        {
            var all = (await OrderitemRepo.GetAllAsync()).Include(o => o.Product).ToList();

            return Maper.Map<List<GetAllOrderItemDTOs>>(all);
        }

        public async Task<ResultView<GetAllOrderItemDTOs>> GetOneAsync(int Id)
        {
            var one = (await OrderitemRepo.GetOneAsync(Id));

            var onemapping = Maper.Map<GetAllOrderItemDTOs>(one);
            ResultView<GetAllOrderItemDTOs> res = new()
            {
                Entity = onemapping,
                IsSuccess = true,
                Message = "Success!"

            };


            return res;

        }



        public async Task<ResultView<CreateOrUpdateOrderItemDTOs>> UpdateAsync(CreateOrUpdateOrderItemDTOs entity)
        {
            ResultView<CreateOrUpdateOrderItemDTOs> result = new();
            try
            {



                var ex = (await OrderitemRepo.GetOneAsync(entity.Id));


                if (ex != null)
                {

                    var prd = Maper.Map<OrderItem>(entity);
                    var SucessEntity = await OrderitemRepo.UpdateAsync(prd);
                    await OrderitemRepo.SaveChanges();
                    var prdchange = Maper.Map<CreateOrUpdateOrderItemDTOs>(SucessEntity);

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





        public async Task<List<GetAllOrderItemDTOs>> GetAllItemsAsync(int Id)
        {
            var all = (await OrderitemRepo.GetAllAsync()).Where(p => p.OrderId == Id).Include(o => o.Product).ThenInclude(i=>i.Images).ToList();

            List<GetAllOrderItemDTOs> result = new();



            foreach (OrderItem item in all)
            {
                GetAllOrderItemDTOs newone = new GetAllOrderItemDTOs()
                {
                    Id = item.Id,
                    PrdDesc = item.Product.Description_en,
                    PrdImage = item.Product.Images.FirstOrDefault().Image,
                    PrdName = item.Product.Title_en,
                    PrdPrice = item.Product.Price,
                    Quantity = item.Quantity,
                    PrdImages = item.Product.Images.Select(p=>p.Image).ToList(),
                };


                result.Add(newone);



            }

            return result;
        }







        //in Dashborad 
        public async Task<List<GetAllOrderItemDTOs>> GetAllWithPrdAsync()
        {
            var all = (await OrderitemRepo.GetAllAsync()).Select(o=>new GetAllOrderItemDTOs
            {
                PrdName = o.Product.Title_en , 
                PrdImages = o.Product.Images.Select(i=>i.Image).ToList() , 
                PrdPrice= o.Product.Price,
                Quantity = o.Quantity,
                TotalPrice = o.Price
               
                
            }).ToList();

            return all;
        }

        public  List<GetAllOitemWithHistory> GetAllOrderItemHistory()
        {
            return OrderitemRepo.GetAllOrderItemHistory();
        }





    }
}
