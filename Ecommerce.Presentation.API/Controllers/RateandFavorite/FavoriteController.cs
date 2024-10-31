using Ecommerce.Application.Services.FavortandRateService;
using Ecommerce.DTOs.DTOsFavoritandRate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.API.Controllers.RateandFavorite
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteServices favoriteServices;
        public FavoriteController(IFavoriteServices _favoriteServices)
        {
            favoriteServices = _favoriteServices;
        }
        // get the favort list for some customer
        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetCustomerFivorit(string CustomerId)
        {
            return Ok((await favoriteServices.AllFavoritbycustomer(CustomerId)));
        }
        // get the most product that chosen as faorite
        [HttpGet]
        public async Task<IActionResult> ThemosteFavoiteProducts()
        {
            return Ok(await favoriteServices.ThemostFavoritproducts());
        }

        // add new fivor for some customer
        [HttpPost]
        public async Task<IActionResult> AddNewFavorite(CreateFavoriteDTOs Entity)
        {
            if (ModelState.IsValid)
            {
                var res = (await favoriteServices.AddToFavoritAsync(Entity));
                if (res.IsSuccess)
                {
                    return Ok(res);
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
        // delete product from favorlits 
        [HttpDelete("{customerId}/{productId:int}")]
        public async Task<IActionResult> DeleteFavorit(string customerId,int productId)
        {

            var res=await favoriteServices.DeleteFavoritAsync(customerId, productId);
            if (res.IsSuccess)
            {
                return Ok(res.Message);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
