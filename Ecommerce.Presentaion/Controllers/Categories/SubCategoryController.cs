﻿using AutoMapper;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.DTOs.DTOsCategories;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers.Categories
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryServices subcategoryService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        public SubCategoryController(ISubCategoryServices _subsubcategoryService, ICategoryService _categoryService, IMapper _mapper)
        {
            subcategoryService = _subsubcategoryService;
            categoryService = _categoryService;
            mapper = _mapper;

        }
        [HttpGet]
        public async Task<IActionResult> AllSubCategories()
        {
            var category = (await subcategoryService.GetAllSubCategoriesAsync());
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           
            ViewBag.Categories = await categoryService.GetAllCategoriesAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateorUpdatedSubCategoryDTOs subcategory)
        {
            if (ModelState.IsValid)
            {
                var result = (await subcategoryService.CreateSubCategpryAsync(subcategory));
                if (result.IsSuccess)
                {
                    return RedirectToAction("AllSubCategories", "SubCategory");
                }
                else { return View(); }
            }
            else { return View(); }
        }
        [HttpGet]
        public async Task<IActionResult> UpdatedSubcategory(int id)
        {
            ViewBag.Categories = await categoryService.GetAllCategoriesAsync();

            var category = (await subcategoryService.GetAllSubCategoriesAsync()).FirstOrDefault(cat => cat.Id == id);
           var subcat =mapper.Map<SubCategory>(category);
            var creatsubcat= mapper.Map<CreateorUpdatedSubCategoryDTOs>(subcat);
            return View(creatsubcat);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatedSubcategory(CreateorUpdatedSubCategoryDTOs category)
        {
            if (ModelState.IsValid)
            {
                var result = (await subcategoryService.UpdatedSubCategpryAsync(category));
                if (result.IsSuccess)
                {
                    return RedirectToAction("AllSubCategories", "SubCategory");
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

        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var result = (await subcategoryService.DeleteSubCategoryAsync(id));
            if (result.IsSuccess)
            {
                return RedirectToAction("AllSubCategories");
            }
            else
            {
                ModelState.AddModelError("deleted has ereror: ", result.Message);
                return RedirectToAction("AllSubCategories");
            }
        }
    }
}