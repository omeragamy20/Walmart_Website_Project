using Ecommerce.Application.Services;
using Ecommerce.Application.ServicesO;
using Ecommerce.DTOs.OrderItemDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderItemsController : Controller
    {
        private readonly IOrderItemService _OrderServ;
        private readonly IOrderService _Order;
        private readonly IProductService _Product;

        public OrderItemsController(IOrderItemService OrderServ , IProductService ProductService , IOrderService Oredr)
        {
            _OrderServ = OrderServ;
            _Order = Oredr;
            _Product = ProductService;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> GetAllItems(int Id)
        {

            var x = (await _OrderServ.GetAllItemsAsync(Id)).ToList();
           

            return View(x);
        }




        [HttpGet]
        public async Task< IActionResult> CreateItems()
        {

             var  products = (await _Product.GetAllArAsync());
            ViewBag.prod = products;
            var order = await _Order.GetAllAsyncPagination(1,10);
            ViewBag.or = order;

            
            return View("CreateItems"); 

        }

       

        [HttpPost]
        public async Task<IActionResult> CreateItems(CreateOrUpdateOrderItemDTOs orderdto)
        {
        if (ModelState.IsValid)
        {
                var x= await _OrderServ.CreateAsync(orderdto);
                //return RedirectToAction($"GetAllItems/{orderdto.OrderId}", "OrderItems");
                //return View($"OrderItems/GetAllItems/{orderdto.OrderId}");
        }
            return RedirectToAction("GetAll", "Order");
        }
    }
}
