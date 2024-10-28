using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        [HttpGet("ProductPagination/{CatId:int}")]
        public async Task<IActionResult> GetAllProductbyCategory(int CatId)
        {
            return Ok(await productService.GetAllProductPaginationEnBySubCatIdAsync(CatId,1,6));
        }
    }
}
