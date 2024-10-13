using Ecommerce.Application.ServicesO;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderservice;

        public OrderController(IOrderService orderService )
        {
            _orderservice = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {


         var All =_orderservice.GetAllAsync();



            return View(All);
        }
        


    }
}
