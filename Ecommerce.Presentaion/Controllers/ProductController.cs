using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Product_Facility;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ISubCategoryServices subCategoryService;
        private readonly IImageService imageService;
        public ProductController(ISubCategoryServices _subCategoryService,IProductService _productService, IImageService _imageService)
        {
            productService = _productService;
            subCategoryService = _subCategoryService;
            imageService = _imageService;
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
                    return RedirectToAction("GetALlProduct");
                }
                return RedirectToAction("GetALlProduct");
            }
            var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
            ViewBag.subcategories = subcategories;
            return View(productDTO);
            
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
                    return RedirectToAction("GetALlProduct");
                }
                return RedirectToAction("GetALlProduct");
            }
            var subcategories = await subCategoryService.GetAllSubCategoriesAsync();
            ViewBag.subcategories = subcategories;
            return View(productDTO);
            
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
