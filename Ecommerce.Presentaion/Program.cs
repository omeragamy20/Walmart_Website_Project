using Ecommerce.Application.Contracts.Categories;
using Ecommerce.Infrastructure.Categories;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Mappper;
using Ecommerce.Application.Services;
using Ecommerce.Context;
using Ecommerce.Infrastructure;
using Ecommerce.Models;
using Ecommerce.Presentaion.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Application.Contracts.product_Facillity;
using Ecommerce.Application.Services.Product_Facility;
using Ecommerce.Infrastructure.Product_Faciity;



namespace Ecommerce.Presentaion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ICategoryService, CategoryServices>();
            builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
            builder.Services.AddScoped<ICategoryReposatiry,CategoryRepository>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
            builder.Services.AddScoped<IFacillityService, FacilityService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<EcommerceContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //builder.Services.AddDefaultIdentity<Customer>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<EcommerceContext>();
            //builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<Customer, IdentityRole>
                (options => {
                    options.SignIn.RequireConfirmedAccount = false;
              
                }

                )
                .AddEntityFrameworkStores<EcommerceContext>() ;
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
            builder.Services.AddScoped<IFacillityService, FacilityService>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
        //pattern: "{controller=Home}/{action=Index}/{id?}");
        pattern: "{controller=Account}/{action=Login}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
