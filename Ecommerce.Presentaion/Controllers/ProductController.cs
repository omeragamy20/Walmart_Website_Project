using AutoMapper;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Product_Facility;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.DTOs.Facility;
using Ecommerce.DTOs.Product;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ecommerce.Presentaion.Controllers
{
    [Authorize(Roles = "admin")]
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
        public async Task<IActionResult> GetALlProduct(int PageNumber=1, int Count=10)
        {
            var products = await productService.GetAllAsync(PageNumber,Count);
           
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

                var oldprd = await productService.GetSubcatbyPrdId(productDTO.Id);
                List<int> oldsubcat=new();
                if (oldprd != null)
                {
                    foreach (var item in oldprd)
                    {
                        oldsubcat.Add( item??0);
                        
                    } 
                }
                var res = await productService.UpdateAsync(productDTO);
                if (res.IsSuccess)
                {

                    bool areEqual = oldsubcat.OrderBy(x => x).SequenceEqual(productDTO.SubCategoryIds.OrderBy(x => x));
                    var imgs = await imageService.UploadImagesAsync(productDTO.ImagesFromFile, res.Entity.Id);
                    if (areEqual)
                    {
                        PrdFacilitySubCategory PFS = new PrdFacilitySubCategory();
                        PFS.ProductId = res.Entity.Id;
                        for (int i = 0; i < productDTO.SubCategoryIds.Count; i++)
                            PFS.SubcatsIDS.Add(productDTO.SubCategoryIds[i]);

                        var prdFacilities = await productfacilityServices.GetFacilitesByPrdIdAsync(res.Entity.Id);
                        PFS.Values_En.AddRange(prdFacilities.Values_En);
                        PFS.Values_Ar.AddRange(prdFacilities.Values_Ar);


                        if (PFS.SubcatsIDS.Count > 0)
                        {
                            var FaciltyDto = await subcatfacilityService.Getallfacilitybyubcatid(PFS.SubcatsIDS);
                            PFS.FacilityDTO = FaciltyDto;
                        }
                        return View("UpdateProductFacilty", PFS);
                    }
                    else
                    {
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
                    }
               
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
                //var oldpfs=await productfacilityServices.GetFacilitesByPrdIdAsync(PFS.ProductId);
                //bool areexist = true;
                //for(int i = 0; i < oldpfs.FacilityDTO.Count; i++)
                //{

                //}
                await productfacilityServices.DeleteFAcilityByPrdId(PFS.ProductId);
                var result = (await productfacilityServices.CreatePrdFaciltyAsync(PFS));
                if (result != null)
                {
                    return RedirectToAction("GetALlProduct");
                }
                else { return View(); }
                //var result = (await productfacilityServices.UpdatePrdFaciltyAsync(PFS));
                //if (result != null)
                //{
                //    return RedirectToAction("GetALlProduct");
                //}
                //else { return View(); }
            }
            else { return View(); }
        }
        public async Task<IActionResult> SearchProduct(string? ProductName, decimal? price,int PageNumber = 1, int Count = 10)
        {
            var product = await productService.SearchByNameAsync(PageNumber,Count,ProductName,price);
            return View("GetALlProduct",product);
        }
        public async Task<IActionResult> Delete(int id)
        {
             var result=await productService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToAction("GetALlProduct");
        }
    }
}
