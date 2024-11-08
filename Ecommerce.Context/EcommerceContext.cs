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
    public class EcommerceContext : IdentityDbContext<Customer>
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

        //public EcommerceContext() { }
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
                new IdentityRole() { Id = "1", Name = "admin" ,NormalizedName="ADMIN"},
                new IdentityRole() { Id = "2", Name = "user", NormalizedName = "USER" }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    UserId = adminUser.Id,
                    RoleId = "1"
                }
                );
            builder.Entity<Category>().HasData(
                     new Category() { Id = 1, Name_en = "Electronics &Video Games", Name_ar = "الإلكترونيات وألعاب الفيديو", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-9674/k2-_cd6b8be4-8bfb-47bc-9843-49e8ed571106.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     new Category() { Id = 2, Name_en = "Clothing & Accessories", Name_ar = "الملابس والإكسسوارات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-48f6/k2-_7aed4b13-f076-4785-8b0c-2a8343c2b70c.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     new Category() { Id = 3, Name_en = "Home", Name_ar = "المنزل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8370/k2-_15a0a4d2-1619-4914-94cd-774567d41404.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     //new Category() { Id = 4, Name_en = "Sports & Outdoors", Name_ar = "الرياضة والهواء الطلق" },
                     new Category() { Id = 4, Name_en = "Patio & Garden", Name_ar = "الفناء والحديقة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-16a3/k2-_f9b2f53c-a2c0-40bf-8e25-692c544b3baf.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     new Category() { Id = 5, Name_en = "Toys", Name_ar = "ألعاب", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-6897/k2-_9d771225-ddc0-4ae4-8302-1921a8ace961.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     new Category() { Id = 6, Name_en = "Grocery", Name_ar = "خضروات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-6406/k2-_987b6e28-ac24-4c30-a150-afe57033daf2.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     //new Category() { Id = 8, Name_en = "Household Essentials", Name_ar = "أساسيات منزلية" },
                     //new Category() { Id = 9, Name_en = "Kitchen & Dining", Name_ar = "المطبخ وتناول الطعام" },
                     new Category() { Id = 7, Name_en = "Personal Care", Name_ar = "العناية الشخصية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-2281/k2-_240ae09f-fd48-4b80-aa8d-7bbed28ae7ea.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     new Category() { Id = 8, Name_en = "Beauty", Name_ar = "الجمال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-5ae9/k2-_d40ab856-ee32-437c-9a44-c9b93df4aff0.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     new Category() { Id = 9, Name_en = "Health & wellness", Name_ar = "الصحة والعافية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-db33/k2-_76752a43-1765-455e-85d2-16a450d8ff5a.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     //new Category() { Id = 13, Name_en = "Kids", Name_ar = "أطفال" },
                     new Category() { Id = 10, Name_en = "Baby", Name_ar = "طفل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-648f/k2-_c76e7139-cecb-4d48-893d-686d9bbbbfbe.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     new Category() { Id = 11, Name_en = "Auto & tires", Name_ar = "السيارات والإطارات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-e091/k2-_5abd632e-14d1-44b2-8361-fd23d6198365.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                     new Category() { Id = 12, Name_en = "Home Improvement", Name_ar = "تحسين المنزل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-a077/k2-_02b361d9-ab7b-45e9-95fb-3060dd6be190.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" }
                 );
            builder.Entity<SubCategory>().HasData(
                new SubCategory() { Id=1,CategoryId=1,Name_en= "TVs", Name_ar= "أجهزة التلفاز" },
                new SubCategory() { Id=2,CategoryId=1,Name_en= "Computers & tablets", Name_ar= "أجهزة الكمبيوتر والأجهزة اللوحية" },
                new SubCategory() { Id=3,CategoryId=1,Name_en= "Headphones & speakers", Name_ar= "سماعات الرأس ومكبرات الصوت" },
                new SubCategory() { Id=4,CategoryId=1,Name_en= "Home theater", Name_ar= "مسرح منزلي" },
                new SubCategory() { Id=5,CategoryId=1,Name_en= "Smart home & networking", Name_ar= "المنزل الذكي والشبكات" },
                new SubCategory() { Id=6,CategoryId=1,Name_en= "PC gaming", Name_ar= "ألعاب الكمبيوتر" },
                new SubCategory() { Id=7,CategoryId=1,Name_en= "Cell phones & wearables", Name_ar= "الهواتف المحمولة والأجهزة القابلة للارتداء" },
                new SubCategory() { Id=8,CategoryId=1,Name_en= "Cameras & drones", Name_ar= "الكاميرات والطائرات بدون طيار" },
                new SubCategory() { Id=9,CategoryId=1,Name_en= "Electronics accessories", Name_ar= "ملحقات الالكترونيات" },
                new SubCategory() { Id=10,CategoryId=1,Name_en= "Video games, movies, music, & books", Name_ar= "ألعاب الفيديو والأفلام والموسيقى والكتب" },
                new SubCategory() { Id=11,CategoryId=1,Name_en= "Xbox", Name_ar= "اكس بوكس" },
                new SubCategory() { Id=12,CategoryId=1,Name_en= "PlayStation", Name_ar= "بلاي ستيشن" },
                new SubCategory() { Id=13,CategoryId=1,Name_en= "Nintendo", Name_ar= "نينتندو" },
                new SubCategory() { Id=14,CategoryId=1,Name_en= "Gaming room", Name_ar= "غرفة الألعاب" }
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
                    entity.Entity.CreatedBy = 1;
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
                    entity.Entity.CreatedBy = 1;
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
