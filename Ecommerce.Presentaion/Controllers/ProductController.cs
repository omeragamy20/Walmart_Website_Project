using Ecommerce.Application.Services;
using Ecommerce.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
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
        public IActionResult Create()
        {
            CreateAndUpdateProductDTO productDTO = new CreateAndUpdateProductDTO();
            return View(productDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var res=await productService.CreateAsync(productDTO);
                if (res.IsSuccess)
                {
                    return RedirectToAction("GetALlProduct");
                }
                return RedirectToAction("GetALlProduct");
            }
            else
            {
                return View(productDTO);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var product = (await productService.GetById(id));
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
            else
            {
                return View(productDTO);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteAsync(id);
            return RedirectToAction("GetALlProduct");
        }
    }
}
