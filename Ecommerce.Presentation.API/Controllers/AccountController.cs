﻿using AutoMapper;
using Ecommerce.DTOs.CustomerDto;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        private readonly UserManager<Customer> userManger;

        private readonly SignInManager<Customer> signInManager;

        public AccountController(IConfiguration _configuration, IWebHostEnvironment _webHostEnvironment, RoleManager<IdentityRole> _roleManager, IMapper _mapper, UserManager<Customer> _userManager, SignInManager<Customer> _signInManager)
        {
            configuration = _configuration;
            this.webHostEnvironment = _webHostEnvironment;
            roleManager = _roleManager;
            mapper = _mapper;
            userManger = _userManager;
            signInManager = _signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users = (await userManger.GetUsersInRoleAsync("user")).Select(u => new GetAllUsersDto
            {
                id = u.Id,
                Username = u.UserName , 
                Email= u.Email , 
                LastName= u.LastName,
                FirstName = u.FirstName,
                Address = u.Address , 
                PhoneNumber = u.PhoneNumber,
                
            }); 
      
            if (users == null) {
                return NotFound();
            }
            return Ok(users); 
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(CustomerLoginDto customerLogin)
        {
            if (ModelState.IsValid)
            {
                Customer user = await userManger.FindByNameAsync(customerLogin.Username);
                if (user != null)
                {
                    var check = await userManger.CheckPasswordAsync(user, customerLogin.Paswword);
                    if (check)
                    {
                        await signInManager.SignInAsync(user, customerLogin.RememberMe);

                        //Generate Tokken 

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));


                        var SignInKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                configuration["JWT:SecritKey"]));

                        SigningCredentials signingCred =
                            new SigningCredentials
                            (SignInKey, SecurityAlgorithms.HmacSha256);


                        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(

                            issuer: configuration["JWT:IssuerIP"],
                            audience: configuration["JWT:AudienceIP"],
                            expires: DateTime.Now.AddHours(1),
                            claims: claims,
                            signingCredentials: signingCred



                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                            expiration = DateTime.Now.AddHours(1)//mytoken.ValidTo
                                                                 //
                        });
                    }
                    return BadRequest();
                }

            }
            return BadRequest();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]CreateCustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {

                var customer = mapper.Map<Customer>(customerDto);

                var res = await userManger.CreateAsync(customer, customerDto.Password);
                await userManger.AddToRoleAsync(customer, "user");
                var role = await roleManager.FindByNameAsync("user");
                if (res.Succeeded)
                {

                    return Ok("Created");
                }
                else
                {
                    foreach (var errorItem in res.Errors)
                        ModelState.AddModelError("", errorItem.Description);
                }
            }
            return BadRequest(ModelState);
        }
    }
}