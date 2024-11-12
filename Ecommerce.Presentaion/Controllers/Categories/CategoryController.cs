using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.DTOs.DTOsCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers.Categories
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;

        }
        [HttpGet]
        public async Task<IActionResult> AllCategories()
        {
            var category=(await categoryService.GetAllCategoriesAsync());
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateorUpdatedCategoryDTOs category)
        {
            if (ModelState.IsValid)
            {
                var result = (await categoryService.CreateCategpryAsync(category));
                if (result.IsSuccess)
                {
                    return RedirectToAction("AllCategories", "Category");
                }
                else { return View(); }
            }
            else { return View(); }
        }
        [HttpGet]
        public async Task<IActionResult> Updatedcategory(int id)
        {
            var category = (await categoryService.GetAllCategoriesAsync()).FirstOrDefault(cat => cat.Id == id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Updatedcategory(CreateorUpdatedCategoryDTOs category)
        {
            if (ModelState.IsValid)
            {
                var result = (await categoryService.UpdatedCategpryAsync(category));
                if (result.IsSuccess)
                {
                    return RedirectToAction("AllCategories");
                    //return View("Categories");
                }
                else
                {
                    return View(category);

                }
            }
            else
            {
                return View(category);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = (await categoryService.DeleteCategoryAsync(id));
            if (result.IsSuccess)
            {
                return RedirectToAction("AllCategories");
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("AllCategories" );
            }
        }
    }
}
