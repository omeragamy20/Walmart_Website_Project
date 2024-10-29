using Ecommerce.Application.Contracts;
using Ecommerce.Application.Contracts.Categories;
using Ecommerce.Application.Contracts.product_Facillity;
using Ecommerce.Application.Mapper;
using Ecommerce.Application.Service;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Product_Facility;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.Application.ServicesO;
using Ecommerce.Context;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Categories;
using Ecommerce.Infrastructure.Product_Faciity;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

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

            builder.Services.AddIdentity<Customer, IdentityRole>
               (options => {
                   options.SignIn.RequireConfirmedAccount = false;
               }).AddEntityFrameworkStores<EcommerceContext>();

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

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
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
