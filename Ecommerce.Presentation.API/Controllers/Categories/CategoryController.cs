using Ecommerce.Application.Services.ServicesCategories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Ecommerce.Presentation.API.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService= _categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok((await categoryService.GetAllCategoriesAsync()).ToList());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok((await categoryService.GetAllCategoriesAsync()).FirstOrDefault(c=>c.Id==id));
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok((await categoryService.GetAllCategoriesAsync())
                .FirstOrDefault(c=>c.Name_ar.Contains(name) || c.Name_en.Contains(name)));
        }
    }
}
