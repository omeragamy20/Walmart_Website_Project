using AutoMapper;
using Ecommerce.DTOs.CustomerDto;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.Presentaion.Controllers.Identity
{

    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        private readonly UserManager<Customer> userManger;

        private readonly SignInManager<Customer> signInManager;

        public AccountController(IWebHostEnvironment _webHostEnvironment , RoleManager<IdentityRole> _roleManager,IMapper _mapper,UserManager<Customer> _userManager , SignInManager<Customer> _signInManager)
        {
            this.webHostEnvironment = _webHostEnvironment;
            roleManager = _roleManager;
            mapper = _mapper;
            userManger = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddAdmin(CreateAdminDto AdminDto)
        {
            if (ModelState.IsValid)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/profile");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + AdminDto.ImageData.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                // Save the image to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await AdminDto.ImageData.CopyToAsync(fileStream);
                }

                var admin = mapper.Map<Customer>(AdminDto);
                admin.Image = $"images/profile/{uniqueFileName}";
                var res = await userManger.CreateAsync(admin ,AdminDto.Password);
               await userManger.AddToRoleAsync(admin, "admin");

                var role = await roleManager.FindByNameAsync("admin"); 
                if(role.Name == "admin" && res.Succeeded) 
                { 
                   var res1 =   await userManger.AddToRoleAsync(admin , role.Name);
                        if (res1.Succeeded)
                        {
                            return RedirectToAction("Login");
                        }
                }
                else
                {
                    foreach (var errorItem in res.Errors)
                        ModelState.AddModelError("",errorItem.Description);
                }
            }
           
            return View(AdminDto);
        }
        [HttpGet]

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(CustomerLoginDto customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer user = await userManger.FindByNameAsync(customer.Username);
                    if (user != null)
                    {
                        var check = await userManger.CheckPasswordAsync(user, customer.Paswword);
                        var role = await userManger.GetRolesAsync(user);
                        if (check == true && role.Contains("admin"))
                        {
                            await signInManager.SignInAsync(user, customer.RememberMe);
                            return RedirectToAction("Index", "Home");
                        }
                    }

                }
                ModelState.AddModelError("", "Invalid Login");
                return RedirectToAction("Login");

            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", "Error");
                return View(customer);
            }
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var admin = await userManger.FindByNameAsync(User.Identity.Name); 
            if(admin != null)
            {
                var adminDto = mapper.Map<GetAdminDto>(admin);
                return View(adminDto);
            }
            return RedirectToAction("Index","Home"); 
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update(string id)
        {
            var admin  = await userManger.FindByIdAsync(id);
            if(admin != null)
            {
                var adminDto = mapper.Map<UpdataAdminDto>(admin);
                return View(adminDto);
            }
            else
            {
               return RedirectToAction("Profile","Account");
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdataAdminDto adminDto)
        {
            if (ModelState.IsValid)
            {
               

                var admin = await userManger.FindByIdAsync(adminDto.Id);
                admin.PhoneNumber = adminDto.PhoneNumber;
                admin.FirstName = adminDto.FirstName;
                admin.LastName = adminDto.LastName;
                admin.Address = adminDto.Address;
                admin.Email = adminDto.Email;

                if(adminDto.ImageData != null) { 
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/profile");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + adminDto.ImageData.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                // Save the image to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await adminDto.ImageData.CopyToAsync(fileStream);
                }

                admin.Image = $"images/profile/{uniqueFileName}";
                }
                var res = await userManger.UpdateAsync(admin);


                if (res.Succeeded)
                {
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    foreach (var errorItem in res.Errors)
                        ModelState.AddModelError("", errorItem.Description);
                }
            }
            return View(adminDto);
        }


        [HttpGet]
        [Authorize]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto passwordDto)
        {
            if (ModelState.IsValid)
            {
                var admin = await userManger.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (admin != null)
                {
                    bool Checked = await userManger.CheckPasswordAsync(admin, passwordDto.OldPassword);
                    if (Checked) 
                    {
                        var hasher = new PasswordHasher<Customer>();
                        admin.PasswordHash = hasher.HashPassword(admin,passwordDto.NewPassword);
                        var res = await userManger.UpdateAsync(admin);
                        if (res.Succeeded) 
                        { 
                            return RedirectToAction("Profile", "Account"); 
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Old Password Isn`t Correct");
                    }

                }

            }
            return View();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
