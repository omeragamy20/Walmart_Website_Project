using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Context
{
    public class EcommerceContext:IdentityDbContext<Customer>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Order> TheOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }
        public DbSet<ProductFacility> ProductFacilities { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<subCatFacility> subCatFacility { get; set; }

        public EcommerceContext() { }
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {
            
        
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var hasher = new PasswordHasher<Customer>();

            // Create the admin user
            var adminUser = new Customer
            {
                Id = "1",
                UserName = "admin",
                NormalizedUserName="ADMIN",
                Address = "sohag",
                FirstName = "admin",
                LastName = "admin",
                PhoneNumber = "01111690167",
                Email = "ahmedbahgat@gmail.com"
            };

            // Hash the password
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin");


            builder.Entity<Customer>().HasData(adminUser);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "admin" },
                new IdentityRole() { Id = "2", Name = "user" }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    UserId = adminUser.Id,
                    RoleId = "1"
                }
                );
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entities = ChangeTracker.Entries<BaseEntity<int>>();

            foreach (var entity in entities)
            {
                if
                (entity.State == EntityState.Added)
                {
                    entity.Entity.Created = DateTime.UtcNow;
                    entity.Entity.CreatedBy =1;
                }
                if
                (entity.State == EntityState.Modified)
                {
                    entity.Entity.Updated = DateTime.UtcNow;
                    entity.Entity.UpdatedBy = 1;
                }
            }
            // set value for base entity data 
            //ChangeTracker.Entries
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // set value for base entity data 
            var entities = ChangeTracker.Entries<BaseEntity<int>>();

            foreach (var entity in entities)
            {
                if
                (entity.State == EntityState.Added)
                {
                    entity.Entity.Created = DateTime.UtcNow;
                    entity.Entity.CreatedBy =1;
                }
                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.Updated = DateTime.UtcNow;
                    entity.Entity.UpdatedBy = 1;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
   
}
