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
            builder.Entity<Category>().HasData(
                    new Category() { Id = 1, Name_en = "Electronics &Video Games", Name_ar = "الإلكترونيات وألعاب الفيديو" },
                    new Category() { Id = 2, Name_en = "Clothing & Accessories", Name_ar = "الملابس والإكسسوارات" },
                    new Category() { Id = 3, Name_en = "Home", Name_ar = "المنزل" },
                    new Category() { Id = 4, Name_en = "Sports & Outdoors", Name_ar = "الرياضة والهواء الطلق" },
                    new Category() { Id = 5, Name_en = "Pools & Hot Tubs", Name_ar = "حمامات السباحة وأحواض المياه الساخنة" },
                    new Category() { Id = 6, Name_en = "Toys", Name_ar = "ألعاب" },
                    new Category() { Id = 7, Name_en = "Food", Name_ar = "طعام" },
                    new Category() { Id = 8, Name_en = "Household Essentials", Name_ar = "أساسيات منزلية" },
                    new Category() { Id = 9, Name_en = "Kitchen & Dining", Name_ar = "المطبخ وتناول الطعام" },
                    new Category() { Id = 10, Name_en = "School & Office Supplies", Name_ar = "اللوازم المدرسية والمكتبية" },
                    new Category() { Id = 11, Name_en = "Beauty", Name_ar = "الجمال" },
                    new Category() { Id = 12, Name_en = "Wellness & Personal Care", Name_ar = "العافية والعناية الشخصية" },
                    new Category() { Id = 13, Name_en = "Kids", Name_ar = "أطفال" },
                    new Category() { Id = 14, Name_en = "Baby", Name_ar = "طفل" },
                    new Category() { Id = 15, Name_en = "Pets", Name_ar = "حيوانات أليفة" },
                    new Category() { Id = 16, Name_en = "Books, Movies & Music", Name_ar = "الكتب والأفلام والموسيقى" }
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
