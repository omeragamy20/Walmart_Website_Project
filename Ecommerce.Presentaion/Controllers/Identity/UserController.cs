using AutoMapper;
using Ecommerce.DTOs.CustomerDto;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentaion.Controllers.Identity
{
    [Authorize(Roles ="admin")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        private readonly UserManager<Customer> userManger;

        private readonly SignInManager<Customer> signInManager;

        public UserController(RoleManager<IdentityRole> _roleManager, IMapper _mapper, UserManager<Customer> _userManager, SignInManager<Customer> _signInManager)
        {
            roleManager = _roleManager;
            mapper = _mapper;
            userManger = _userManager;
            signInManager = _signInManager;
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            var users = await userManger.GetUsersInRoleAsync("user");
            var customers = mapper.Map<List<GetAllUsersDto>>(users);
            return View(customers);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await userManger.FindByIdAsync(id);
            if(user != null)
            {
                var res = await userManger.DeleteAsync(user);
                if (res.Succeeded)
                    return RedirectToAction("Index");
            }

               ModelState.AddModelError("", "Not Deleted");
                return View("Index");
            
        }
        
    }
}
