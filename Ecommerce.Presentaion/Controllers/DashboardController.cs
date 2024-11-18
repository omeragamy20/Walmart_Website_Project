using Ecommerce.Application.Services;
using Ecommerce.Application.ServicesO;
using Ecommerce.DTOs.Dashboard;
using Ecommerce.DTOs.OrderDTOs;
using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IOrderItemService OrderItemServ;
        private readonly IOrderService OrderSer;
        private readonly IProductService ProductSer;
        private readonly UserManager<Customer> userManger;

        public DashboardController(UserManager<Customer> _userManager, IOrderItemService _OrderItemServ, IProductService ProductService, IOrderService _OrderService)
        {
            OrderItemServ = _OrderItemServ;
            OrderSer = _OrderService;
            ProductSer = ProductService;
            userManger = _userManager;
        }
        public async Task<IActionResult> Index()
         {
            //carts  - sales

            TempData["SalesToday"] = GetAllOrderItemthird();
            TempData["SalesYesterdat"] = GetAllOrderItemsecond();
            TempData["Salesbefor"] = GetAllOrderItemFirst();


            //Revenue 
            ViewBag.CostToday = GetAllOrderItemCostToday();
            ViewBag.CostYesterday = GetAllOrderItemCostYesterDay();
            ViewBag.CostBefor = GetAllOrderItemCostBefor();


            // Sales => Total Product * quantity 
            var TotalorderItem =  await GetAllOrderItem();
            TempData["Sales"] = TotalorderItem.ToString();
            
            // Total Sales cost 
            var TotalorderItemCost = await GetAllOrderItemCost();
            TempData["Revenue"] = TotalorderItemCost.ToString();


            var AllCustomers = await GetAllCustomer();
            TempData["Customers"] = AllCustomers.ToString();

            var latestOrder = await RecentSales();
            ViewBag.lastOrders = latestOrder;


            var topSelleing = await TopSelling();
            return View(topSelleing);
        }


        public async Task<int> GetAllOrderItem()
        {
            var orderItems = await OrderItemServ.GetAllAsync();

            return orderItems.Select(p=>p.Quantity).ToList().Sum(); 

        }


        public async Task<decimal> GetAllOrderItemCost()
        {
            var orderItems = await OrderItemServ.GetAllAsync();

            return orderItems.Select(p => p.Price).ToList().Sum() * .1M;

        }


        public async Task<int> GetAllCustomer()
        {
            var users = await userManger.GetUsersInRoleAsync("user");

            return users.Count() ;

        }



        public async Task<List<TopSellingProductDTO>> TopSelling()
        {
            var orders = (await OrderItemServ.GetAllWithPrdAsync());
            var RecentOrderItems = orders.GroupBy(o => o.PrdName).Select(a => new TopSellingProductDTO
            {
                ProductName = a.Key,
                TotalQuantity = a.Sum(o => o.Quantity),
                price = a.Select(o => o.PrdPrice).Max(),
                TotalCost = a.Select(o => o.TotalPricee).Sum() * .1M,
                image = a.Select(i => i.PrdImages.FirstOrDefault()).FirstOrDefault() ?? " ",
            }).OrderByDescending(o => o.TotalQuantity).Take(5).ToList();
            return RecentOrderItems;

        }

        public async Task<List<GetAllOrderDTOs>> RecentSales()
        {
           var latestOrder =  (await OrderSer.GetAllAsync()).OrderByDescending(o=>o.OrderDate).ToList();
            return latestOrder; 
        }

        //============================================================
        // for charts    

        // For customer 
        //public async Task<int> GetAllCustomerToday()
        //{
        //    var users = (await userManger.GetUsersInRoleAsync("user")).Where(u=>u.);

        //    return users.Count();

        //}


        //for sales
        public int GetAllOrderItemFirst()
        {
            var orderItemsToday = OrderItemServ.GetAllOrderItemHistory().Where(o=>o.CreatedAt.Date == DateTime.Today.AddDays(-2)).Count();

            return orderItemsToday;

        }
        public int GetAllOrderItemsecond()
        {
            var orderItemsToday = OrderItemServ.GetAllOrderItemHistory().Where(o => o.CreatedAt.Date == DateTime.Today.AddDays(-1)).Count();

            return orderItemsToday;

        }
        public int GetAllOrderItemthird()
        {
            var orderItemsToday = OrderItemServ.GetAllOrderItemHistory().Where(o => o.CreatedAt.Date == DateTime.Today).Count();

            return orderItemsToday;

        }


        //for revenu 

        public decimal GetAllOrderItemCostToday()
        {
            var orderItems =  OrderItemServ.GetAllOrderItemHistory().Where(o => o.CreatedAt.Date == DateTime.Today) ;

            var cost = orderItems.Select(p => p.Price).ToList().Sum() * .1M;
            return cost;

        }


        public decimal GetAllOrderItemCostYesterDay()
        {
            var orderItems = OrderItemServ.GetAllOrderItemHistory().Where(o => o.CreatedAt.Date == DateTime.Today.AddDays(-1));
            var cost = orderItems.Select(p => p.Price).ToList().Sum() * .1M;
            return  cost;

        }
        public decimal GetAllOrderItemCostBefor()
        {
            var orderItems = OrderItemServ.GetAllOrderItemHistory().Where(o => o.CreatedAt.Date == DateTime.Today.AddDays(-2));

            var cost = orderItems.Select(p => p.Price).ToList().Sum() * .1M;
            return cost;

        }


    }
}
