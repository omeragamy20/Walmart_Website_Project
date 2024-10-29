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
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace Ecommerce.Presentation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<EcommerceContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IShaipmentRepository, ShipmentRepository>();
            builder.Services.AddScoped<IShipmentService, ShipmentServices>();


            builder.Services.AddScoped<IPaymentRepoistory, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, Paymentservice>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


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
