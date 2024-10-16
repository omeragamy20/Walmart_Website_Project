using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Context
{
    public class EcommerceContext:IdentityDbContext<Customer>
    {
        UserManager<Customer> _userManager;
        // the models sets
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

        //public DbSet<Status> Statuses { get; set; }


        public EcommerceContext() { }
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        { }
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
