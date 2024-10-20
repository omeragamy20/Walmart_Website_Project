﻿using AutoMapper;
using Ecommerce.Application.Contracts;
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
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemsReposatiry OrderitemRepo;
        private readonly IMapper Maper;

        public OrderItemService(IOrderItemsReposatiry _OrderitemRepo , IMapper _Maper)
        {
            OrderitemRepo = _OrderitemRepo;
            Maper = _Maper;
        }

        public async Task<ResultView<CreateOrUpdateOrderItemDTOs>> CreateAsync(CreateOrUpdateOrderItemDTOs entity)
        {
            ResultView<CreateOrUpdateOrderItemDTOs> result = new();
            try
            {


                bool Exist = (await OrderitemRepo.GetAllAsync()).Any(p => p.Id == entity.Id);

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

                var prd = Maper.Map<OrderItem>(entity);
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
            var all = (await OrderitemRepo.GetAllAsync()).ToList();

            return Maper.Map<List<GetAllOrderItemDTOs>>(all);
        }

        public async Task<ResultView<GetAllOrderItemDTOs>> GetOneAsync(int Id)
        {
            var one = await OrderitemRepo.GetOneAsync(Id);

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
    }
}
