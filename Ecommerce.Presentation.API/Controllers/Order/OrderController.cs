using Ecommerce.Application.ServicesO;
using Ecommerce.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerce.Presentation.API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderserv;

        public OrderController(IOrderService _orderserv)
        {
            orderserv = _orderserv;
        }




        [HttpGet]
        public async Task<IActionResult>Get()
        {

            var all = await orderserv.GetAllAsync();

            return Ok(all);

        }



        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> Get(int id)
        {
          

            var one = await orderserv.GetOneAsync(id);
            return Ok(one);

        }

        [HttpGet("Customer/{CustomerId}")]
        public  IActionResult GetOrdersByCustomer(string CustomerId)
        {
            var one =  orderserv.GetOrdersByCusId(CustomerId);
            if (one == null)
            {
                return NotFound();
            }
            return Ok(one);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateOrUpdateOrderDTOs dto)
        {
            var creat = await orderserv.CreateAsync(dto);
            return Ok(creat);
        }



        [HttpPut]
        public async Task<IActionResult> Update(CreateOrUpdateOrderDTOs dto)
        {

            var updat = await orderserv.UpdateAsync(dto);


            return Ok(updat);
        }

        [HttpDelete]
        public async Task<IActionResult>Delete(int id)
        {
            var delete = await orderserv.DeleteAsync(id);
            return Ok(delete);

        }


    }
}
