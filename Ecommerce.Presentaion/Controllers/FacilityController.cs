using Ecommerce.Application.Services;
using Ecommerce.DTOs.Facility;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers
{
    public class FacilityController : Controller
    {
        private readonly IFacillityService facilityService;

        public FacilityController(IFacillityService _facillityService)
        {
            facilityService = _facillityService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetALl()
        {
            var facilities = await facilityService.GetAllAsync();
            return View(facilities);
        }
        public IActionResult Create()
        {
            FacilityDTO facilityDTO = new FacilityDTO();
            return View(facilityDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create(FacilityDTO facilityDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await facilityService.CreateAsync(facilityDTO);
                if (res.IsSuccess)
                {
                    return RedirectToAction("GetALl");
                }
                return RedirectToAction("GetALl");
            }
            else
            {
                return View(facilityDTO);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var facility = await facilityService.GetByIdAsync(id);
            return View(facility);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FacilityDTO facilityDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await facilityService.UpdateAsync(facilityDTO);
                if (res.IsSuccess)
                {
                    return RedirectToAction("GetALl");
                }
                return RedirectToAction("GetALl");
            }
            else
            {
                return View(facilityDTO);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await facilityService.DeleteAsync(id);
            return RedirectToAction("GetALl");
        }
    }
}
