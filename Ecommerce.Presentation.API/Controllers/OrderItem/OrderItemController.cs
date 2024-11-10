using Ecommerce.Application.ServicesO;
using Ecommerce.DTOs.OrderItemDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.API.Controllers.OrderItem
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService orderitemserv;

        public OrderItemController(IOrderItemService _orderitemserv)
        {
            orderitemserv = _orderitemserv;

        }



        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            if (ModelState.IsValid)
            {
            var all = await orderitemserv.GetAllItemsAsync(Id);

            return Ok(all);
            }
            return BadRequest();
        }




        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateOrUpdateOrderItemDTOs dto)
        {

            if (ModelState.IsValid)
            {
            var cret = await orderitemserv.CreateAsync(dto);
                return Ok(cret);
            }
            return BadRequest();

        }




        [HttpPut]
        public async Task<IActionResult> Update(CreateOrUpdateOrderItemDTOs dto)
        {
            if (!ModelState.IsValid)
            {
                var up = await orderitemserv.UpdateAsync(dto);
                return Ok(up);

            }
            return BadRequest();

        }



        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var del = await orderitemserv.DeleteAsync(Id);
            return Ok(del);
        }


    }
}
