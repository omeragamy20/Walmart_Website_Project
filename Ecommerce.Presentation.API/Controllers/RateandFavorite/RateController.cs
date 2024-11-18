using Ecommerce.Application.Services.FavortandRateService;
using Ecommerce.DTOs.DTOsFavoritandRate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.API.Controllers.RateandFavorite
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateServices rateservices;
        public RateController(IRateServices _rateservices)
        {
            rateservices= _rateservices;
        }
        [HttpPost("CreateRate")]
        public async Task<IActionResult> AddRate(CreateorUpdateRateDTO Entity)
        {
            if (ModelState.IsValid)
            {
                var result=await rateservices.CreateorUpdatedRateAsync(Entity);
                if (result.IsSuccess)
                {
                    return Ok("You Rate This Product Successfuly");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductRate(int productId)
        {
            return Ok(await rateservices.GetAVGProductRateAsync(productId));
        }
    }
}
