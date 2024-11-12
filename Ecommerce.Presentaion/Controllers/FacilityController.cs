using Ecommerce.Application.Services;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.DTOs.Facility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers
{
    [Authorize(Roles = "admin")]
    public class FacilityController : Controller
    {
        private readonly IFacillityService facilityService;
        private readonly ISubCategoryServices subCategoryService;
        public FacilityController(ISubCategoryServices _subCategoryService,IFacillityService _facillityService)
        {
            facilityService = _facillityService;
            subCategoryService = _subCategoryService;
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
        public async Task<IActionResult> Create()
        {
            var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
            ViewBag.subcategories = subcategories;
            //CreatorUpdateFacilityDTO  facilityDTO = new CreatorUpdateFacilityDTO();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatorUpdateFacilityDTO facilityDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await facilityService.CreateAsync(facilityDTO);
                if (res.IsSuccess)
                {
                    return RedirectToAction("GetALl");
                }
                else
                {
                    var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
                    ViewBag.subcategories = subcategories;
                    return View();
                }
                //return RedirectToAction("GetALl");
            }
            else
            {
                var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
                ViewBag.subcategories = subcategories;
                return View();
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
            ViewBag.subcategories = subcategories;
            var facility = await facilityService.GetByIdAsync(id);
            return View(facility);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreatorUpdateFacilityDTO facilityDTO)
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
                var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
                ViewBag.subcategories = subcategories;
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
