using Ecommerce.Application.Services.ServicesCategories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.API.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryServices subcategoryServices;
        private readonly ICategoryService categoryService;
        public SubCategoryController(ISubCategoryServices _subcategoryServices, ICategoryService _categoryService)
        {
            subcategoryServices = _subcategoryServices;
            categoryService = _categoryService;
        }
        // get all subcategory
        [HttpGet]
        public async Task<IActionResult> GetAllSubCategory()
        {
            return Ok((await subcategoryServices.GetAllSubCategoriesAsync()).ToList());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            return Ok((await subcategoryServices.GetAllSubCategoriesAsync()).FirstOrDefault(SC=>SC.Id==id));
        }
        // get all subcategory by category name
        [HttpGet("GetSubCategoryByCategoryName/{CatName}")]
        public async Task<IActionResult> GetSubCategoryByCategoryName(string CatName)
        {
            return Ok((await subcategoryServices.GetAllSubCategoriesAsync())
                .Where(sc=>sc.CategryName_ar.Contains(CatName)|| sc.CategryName_en.Contains(CatName))
                .ToList());
        }

        // get all subcategory by name
        [HttpGet("GetSubCategoryName/{SubCatName}")]
        public async Task<IActionResult> GetSubCategoryName(string SubCatName)
        {
            return Ok((await subcategoryServices.GetAllSubCategoriesAsync())
                .Where(sc=>sc.Name_ar.Contains(SubCatName) || sc.Name_en.Contains(SubCatName))
                .ToList());
        }

        [HttpGet("GetSubCategoryByCatId/{Catid:int}")]
        public async Task<IActionResult> GetSubCategoryByCatId(int Catid)
        {
            return Ok((await subcategoryServices.GetAllSubCategoriesAsync()).Where(cat=>cat.CategoryId==Catid).ToList());
        }

    }
}
