using Ecommerce.Application.Contracts;
using Ecommerce.Application.Contracts.Categories;
using Ecommerce.Infrastructure.Categories;
using Ecommerce.Application.Services.ServicesCategories;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Mappper;
using Ecommerce.Application.ServicesO;
using Ecommerce.Application.Services;
using Ecommerce.Context;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure;
using Ecommerce.Presentaion.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



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


            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<EcommerceContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EcommerceContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
            builder.Services.AddScoped<IFacillityService, FacilityService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderReposatiry, OrderReposatiry>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IOrderItemsReposatiry, OrderItemsReposatiry>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
