using Ecommerce.Application.Services;
using Ecommerce.Application.ServicesO;
using Ecommerce.DTOs.OrderItemDTOs;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.API.Controllers.OrderItem
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService orderitemserv;
        private readonly IProductService _Product;

        public OrderItemController(IOrderItemService _orderitemserv , IProductService ProductService)
        {
            orderitemserv = _orderitemserv;
            _Product = ProductService;

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
        public async Task<IActionResult> Create(CreateOrUpdateOrderItemDTOs orderdto)
        {

            if (ModelState.IsValid)
            {
                var product = await _Product.GetById(orderdto.ProductId ?? 0 );
                if (product != null)
                {
                    if (product.Stock >= orderdto.Quantity && product.Stock - orderdto.Quantity >= 0)
                    { 
                         var cret = await orderitemserv.CreateAsync(orderdto);
                         return Ok(cret);
                    }
                    else
                    {
                        return BadRequest("The Amount isn`t enough");
                    }
                }
               
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
