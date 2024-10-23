using AutoMapper;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Product_Facility;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.DTOs.Facility;
using Ecommerce.DTOs.Product;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ISubCategoryServices subCategoryService;
        private readonly ISubCatFacilityService subcatfacilityService;
        private readonly IFacillityService facilityService;
        private readonly IImageService imageService;
        private readonly IProductFacilityServices productfacilityServices;
        public ProductController(ISubCategoryServices _subCategoryService,
            IProductService _productService,
            IImageService _imageService, IProductFacilityServices _productfacilityServices,
            ISubCatFacilityService _subcatfacilityService, IFacillityService _facilityService)
        {
            productService = _productService;
            subCategoryService = _subCategoryService;
            imageService = _imageService;
            subcatfacilityService= _subcatfacilityService;
            facilityService = _facilityService;
            productfacilityServices= _productfacilityServices;
        }
      
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetALlProduct()
        {
            var products = await productService.GetAllAsync();
           
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
            CreateAndUpdateProductDTO productDTO = new CreateAndUpdateProductDTO();
            ViewBag.subcategories=subcategories;
            return View(productDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateProductDTO productDTO, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {

                var res=await productService.CreateAsync(productDTO);
               
                if (res.IsSuccess)
                {
                    var imgs = await imageService.UploadImagesAsync(productDTO.ImagesFromFile, res.Entity.Id);

                    PrdFacilitySubCategory PFS = new PrdFacilitySubCategory();
                    PFS.ProductId = res.Entity.Id; 
                    for (int i = 0;i < productDTO.SubCategoryIds.Count;i++)
                        PFS.SubcatsIDS.Add(productDTO.SubCategoryIds[i]);

                    if(PFS.SubcatsIDS.Count > 0)
                    {
                      var FaciltyDto = await subcatfacilityService.Getallfacilitybyubcatid(PFS.SubcatsIDS);
                        PFS.FacilityDTO = FaciltyDto;
                      
                    }

                    return View("CreatePrdFacility" , PFS);

                    //await CreatePrdFacility(res.Entity.Id);
                    //return RedirectToAction("CreatePrdFacility", res.Entity.Id);
                    //return RedirectToAction("GetALlProduct");
                }
                return RedirectToAction("GetALlProduct");
            }
            var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
            ViewBag.subcategories = subcategories;
            return View(productDTO);
            
        }
        [HttpGet]
        public async Task<IActionResult> CreatePrdFacility()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrdFacility(PrdFacilitySubCategory PFS)
        {
            if (ModelState.IsValid)
            {
                var result = (await productfacilityServices.CreatePrdFaciltyAsync(PFS));
                if (result!=null)
                {
                    return RedirectToAction("GetALlProduct");
                }
                else { return View(); }
            }
            else { return View(); }
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = (await productService.GetById(id));
            var subcategories = (await subCategoryService.GetAllSubCategoriesAsync());
            ViewBag.subcategories = subcategories;
            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Update(CreateAndUpdateProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await productService.UpdateAsync(productDTO);
                if (res.IsSuccess)
                {
                    var imgs = await imageService.UploadImagesAsync(productDTO.ImagesFromFile, res.Entity.Id);
                    PrdFacilitySubCategory PFS = new PrdFacilitySubCategory();
                    PFS.ProductId = res.Entity.Id;
                    for (int i = 0; i < productDTO.SubCategoryIds.Count; i++)
                        PFS.SubcatsIDS.Add(productDTO.SubCategoryIds[i]);

                    if (PFS.SubcatsIDS.Count > 0)
                    {
                        var FaciltyDto = await subcatfacilityService.Getallfacilitybyubcatid(PFS.SubcatsIDS);
                        PFS.FacilityDTO = FaciltyDto;
                    }
                    return View("UpdateProductFacilty", PFS);
                    return RedirectToAction("GetALlProduct");
                }
                return RedirectToAction("GetALlProduct");
            }
            var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
            ViewBag.subcategories = subcategories;
            return View(productDTO);
            
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProductFacilty() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductFacilty(PrdFacilitySubCategory PFS)
        {

            if (ModelState.IsValid)
            {
                var result = (await productfacilityServices.UpdatePrdFaciltyAsync(PFS));
                if (result != null)
                {
                    return RedirectToAction("GetALlProduct");
                }
                else { return View(); }
            }
            else { return View(); }
        }
        public async Task<IActionResult> SearchProduct(string ProductName)
        {
            var product = await productService.SearchByNameAsync(ProductName);
            return View(product);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteAsync(id);
            return RedirectToAction("GetALlProduct");
        }
    }
}
