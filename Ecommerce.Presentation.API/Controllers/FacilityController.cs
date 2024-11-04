using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Services;
namespace Ecommerce.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        readonly IFacillityService facillityService;
        public FacilityController(IFacillityService _facillityService)
        {
            facillityService = _facillityService;
        }
        [HttpGet("facilities")]
        public async Task<IActionResult> GetFacility(int subid)
        {
            var result = await facillityService.GetAllBySubIdAsync(subid);
            return Ok(result);
        }
    }
}
