using Ecommerce.Application.Contracts;
using Ecommerce.Application.Mapper;
using Ecommerce.Application.Service;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.Context;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Categories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ecommerce.Application.Contracts.Categories;
using Ecommerce.Application.Contracts.FavortandRateRepo;
using Ecommerce.Application.Contracts.product_Facillity;
using Ecommerce.Application.Mapper;
using Ecommerce.Application.Service;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.FavortandRateService;
using Ecommerce.Application.Services.Product_Facility;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.Application.ServicesO;
using Ecommerce.Context;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Categories;
using Ecommerce.Infrastructure.FavortandRate;
using Ecommerce.Infrastructure.Product_Faciity;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.DTOs.PayPalDTOs;
namespace Ecommerce.Presentation.API
{
    public class Program
    {
        public static void Main(string[] args)
        { 

            var builder = WebApplication.CreateBuilder(args);
          
            builder.Services.AddScoped<ICategoryService, CategoryServices>();
            builder.Services.AddScoped<ICategoryReposatiry, CategoryRepository>();

            builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IProductSubCategoryRepository, ProductSubCategoryRepository>();
            builder.Services.AddScoped<IProductFacilityRepository, ProductFacilityRepository>();
            builder.Services.AddScoped<IProductFacilityServices, ProductFacilityServices>();

            builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
            builder.Services.AddScoped<IFacillityService, FacilityService>();

            builder.Services.AddScoped<ISubCatFacilityRepository, SubCatFacilityRepository>();
            builder.Services.AddScoped<ISubCatFacilityService, SubCatFacilityService>();


            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderReposatiry, OrderReposatiry>();

            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IOrderItemsReposatiry, OrderItemsReposatiry>();
            builder.Services.AddScoped<IShipmentService, ShipmentServices>();
            builder.Services.AddScoped<IShaipmentRepository, ShipmentRepository>();


            builder.Services.AddScoped<IPaymentService, Paymentservice>();
            builder.Services.AddScoped<IPaymentRepoistory, PaymentRepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));



            builder.Services.AddScoped<ICategoryService, CategoryServices>();
            builder.Services.AddScoped<ICategoryReposatiry, CategoryRepository>();

            builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IProductSubCategoryRepository, ProductSubCategoryRepository>();
            builder.Services.AddScoped<IProductFacilityRepository, ProductFacilityRepository>();
            builder.Services.AddScoped<IProductFacilityServices, ProductFacilityServices>();

            builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
            builder.Services.AddScoped<IFacillityService, FacilityService>();

            builder.Services.AddScoped<ISubCatFacilityRepository, SubCatFacilityRepository>();
            builder.Services.AddScoped<ISubCatFacilityService, SubCatFacilityService>();

            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderReposatiry, OrderReposatiry>();

            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IOrderItemsReposatiry, OrderItemsReposatiry>();
            builder.Services.AddScoped<IShipmentService, ShipmentServices>();
            builder.Services.AddScoped<IShaipmentRepository, ShipmentRepository>();

            builder.Services.AddScoped<IPaymentService, Paymentservice>();
            builder.Services.AddScoped<IPaymentRepoistory, PaymentRepository>();

            builder.Services.AddScoped<IFavoriteServices,FavoriteServices>();
            builder.Services.AddScoped<IFavoriteRepository,FavoriteRepository>();

            builder.Services.AddScoped<IRateRepository, RateRepository>();
            builder.Services.AddScoped<IRateServices, RateServices>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            //builder.Logging.AddConsole(); // Ensure logging is enabled.


            builder.Services.AddIdentity<Customer, IdentityRole>
              (options => {
                  options.SignIn.RequireConfirmedAccount = false;
              }).AddEntityFrameworkStores<EcommerceContext>()
              .AddDefaultTokenProviders();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<EcommerceContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddMvc().AddNewtonsoftJson();
            builder.Services.AddHttpClient();

            builder.Services.AddCors(op =>
            {
                op.AddPolicy("Default", policy =>
                {

                    policy.AllowAnyHeader()
                           .AllowAnyOrigin()
                           .AllowAnyMethod();
                });
            });


            builder.Services.AddDbContext<EcommerceContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

           


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //});
                app.UseSwaggerUI();
            }

            //kahled
            //app.MapPost("/CreatePayment", ([FromBody] IEnumerable<ItemDto> items, IConfiguration configuration, HttpContext context) =>
            //{
            //    var baseUrl = context.Request.Host.Value;

            //    return new PayPalService(configuration).CreatePayment(items, baseUrl);
            //});

            //app.MapPost("/ExecutePayment", ([FromBody] ExecutePaymentDto dto, IConfiguration configuration) =>
            //{
            //    return new PayPalService(configuration).ExecutePayment(dto);
            //});

            app.UseCors("Default");
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseCors("Default");

            app.MapControllers();

            app.Run();
        }
    }
}
