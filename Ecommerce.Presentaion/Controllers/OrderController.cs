﻿using AutoMapper;
using Ecommerce.Application.ServicesO;
using Ecommerce.Context;
using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.DTOs.shared;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderservice;
        private readonly IMapper mape;
        private readonly EcommerceContext _con;

        public OrderController(IOrderService orderService , IMapper _Maper , EcommerceContext Cont)
        {
            _orderservice = orderService;
            mape = _Maper;
            _con = Cont;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


         var All =( await _orderservice.GetAllAsync()).ToList();



            return View(All);
        }


        [HttpGet("/Approve/{Id}")]
        public async Task<IActionResult> Approve(int Id)
        {
            var order = await _orderservice.GetOneOrderAsync(Id); // Ensure this returns the Order entity

            if (order == null)
            {
                return NotFound();
            }
            
     
            order.Status = 1; 

            _con.TheOrders.Update(order); 
            await _con.SaveChangesAsync(); 

            return RedirectToAction("GetAll");
        }

        [HttpGet("/Cancel/{Id}")]
        public async Task<IActionResult> Cancel (int Id)
        {
            var order = await _orderservice.GetOneOrderAsync(Id); // Ensure this returns the Order entity

            if (order == null)
            {
                return NotFound();
            }


            order.Status = 2;

            _con.TheOrders.Update(order);
            await _con.SaveChangesAsync();

            return RedirectToAction("GetAll");

        }


    }
}
