using Ecommerce.DTOs.CustomerDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers.Identity
{
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> RoleManager { get; }

        public RoleController(RoleManager<IdentityRole> _roleManager) 
        {
            RoleManager = _roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateRoleDto newRole)
        {
            if (ModelState.IsValid) 
            {
                IdentityRole NewRole = new() { Name = newRole.RoleName };
               var IResult = await RoleManager.CreateAsync(NewRole);
                if (IResult.Succeeded)
                {
                    return RedirectToAction("Add");
                }
            }
            return View(newRole);
        }
    }
}
