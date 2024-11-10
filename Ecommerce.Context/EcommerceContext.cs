using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        //}
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
                    new Category() { Id = 1, Name_en = "Electronics &Video Games", Name_ar = "الإلكترونيات وألعاب الفيديو",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-9674/k2-_cd6b8be4-8bfb-47bc-9843-49e8ed571106.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    new Category() { Id = 2, Name_en = "Clothing & Accessories", Name_ar = "الملابس والإكسسوارات",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-48f6/k2-_7aed4b13-f076-4785-8b0c-2a8343c2b70c.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    new Category() { Id = 3, Name_en = "Home", Name_ar = "المنزل",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-8370/k2-_15a0a4d2-1619-4914-94cd-774567d41404.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    //new Category() { Id = 4, Name_en = "Sports & Outdoors", Name_ar = "الرياضة والهواء الطلق" },
                    new Category() { Id = 4, Name_en = "Patio & Garden", Name_ar = "الفناء والحديقة",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-16a3/k2-_f9b2f53c-a2c0-40bf-8e25-692c544b3baf.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    new Category() { Id = 5, Name_en = "Toys", Name_ar = "ألعاب",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-6897/k2-_9d771225-ddc0-4ae4-8302-1921a8ace961.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    new Category() { Id = 6, Name_en = "Grocery", Name_ar = "خضروات",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-6406/k2-_987b6e28-ac24-4c30-a150-afe57033daf2.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    //new Category() { Id = 8, Name_en = "Household Essentials", Name_ar = "أساسيات منزلية" },
                    //new Category() { Id = 9, Name_en = "Kitchen & Dining", Name_ar = "المطبخ وتناول الطعام" },
                    new Category() { Id = 7, Name_en = "Personal Care", Name_ar = "العناية الشخصية",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-2281/k2-_240ae09f-fd48-4b80-aa8d-7bbed28ae7ea.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    new Category() { Id = 8, Name_en = "Beauty", Name_ar = "الجمال",Image="https://i5.walmartimages.com/dfw/4ff9c6c9-5ae9/k2-_d40ab856-ee32-437c-9a44-c9b93df4aff0.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    new Category() { Id = 9, Name_en = "Health & wellness", Name_ar = "الصحة والعافية",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-db33/k2-_76752a43-1765-455e-85d2-16a450d8ff5a.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    //new Category() { Id = 13, Name_en = "Kids", Name_ar = "أطفال" },
                    new Category() { Id = 10, Name_en = "Baby", Name_ar = "طفل",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-648f/k2-_c76e7139-cecb-4d48-893d-686d9bbbbfbe.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    new Category() { Id = 11, Name_en = "Auto & tires", Name_ar = "السيارات والإطارات",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-e091/k2-_5abd632e-14d1-44b2-8361-fd23d6198365.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" },
                    new Category() { Id = 12, Name_en = "Home Improvement", Name_ar = "تحسين المنزل", Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-a077/k2-_02b361d9-ab7b-45e9-95fb-3060dd6be190.v1.jpg?odnHeight=120&odnWidth=120&odnBg=FFFFFF" }
                );
            builder.Entity<SubCategory>().HasData(
                //electronicts
                new SubCategory() { Id=1,CategoryId=1,Name_en= "TVs & Home theater", Name_ar= "أجهزة التلفاز والمسرح المنزلي", Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-ee42/k2-_b2dd1147-d210-44f5-bd28-2c852988673f.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=2,CategoryId=1,Name_en= "Computers & Office", Name_ar= "أجهزة الكمبيوتر والمكتب",Image= "https://i5.walmartimages.com/dfw/4ff9c6c9-7457/k2-_584091c4-0a16-4543-9df7-73ab3826b921.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=3,CategoryId=1,Name_en= "iPads & tablets", Name_ar= "أجهزة iPad والأجهزة اللوحية ", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-4856/k2-_3546e183-7a9b-4b23-9abf-24b0d5312aa5.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=4,CategoryId=1,Name_en= "Cell phones ", Name_ar= "الهواتف المحمولة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-c6d4/k2-_6f2d6b1f-ea5b-40c7-a97b-4cd6a128a2be.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=5,CategoryId=1,Name_en= "Video games", Name_ar= "ألعاب الفيديو", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-3109/k2-_2c84a06e-bc04-4ed4-aa1b-daf1903a5ecc.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=6,CategoryId=1,Name_en= "Headphones", Name_ar= "سماعات الرأس", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8c00/k2-_b705e007-ecf0-404b-86a5-3bb7b06175e3.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=7,CategoryId=1,Name_en= "Smart home", Name_ar= "المنزل الذكي", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-5228/k2-_86a06bdb-7a47-4925-a815-375ea64b844e.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=8,CategoryId=1,Name_en= "Cameras & drones", Name_ar= "الكاميرات والطائرات بدون طيار", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-bd7e/k2-_e3d8ae1d-1bbe-484f-a2bf-ee42f2f3df7c.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=9,CategoryId=1,Name_en= "Wearable Tech", Name_ar= "التكنولوجيا القابلة للارتداء", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-6548/k2-_bdc8e124-6557-46a3-84c5-91f801370354.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=10,CategoryId=1,Name_en= "Electronics accessories", Name_ar= "ملحقات الالكترونيات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-160b/k2-_8f63a817-0501-4ebb-b436-6b135601bbda.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Clothing & Accessories
                new SubCategory() { Id=11,CategoryId=2,Name_en="Women's" , Name_ar= "للنساء", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-7b71/k2-_dabe399b-edc2-4213-ab39-0ec6ee662940.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=12,CategoryId=2,Name_en= "Men's", Name_ar= "للرجال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8685/k2-_814c74ab-6049-40cb-a598-5652ba07d847.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=13,CategoryId=2,Name_en="Young adult" , Name_ar= "شاب بالغ", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-a4ef/k2-_80bf60b4-7259-4d11-959a-b0791b955b5a.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=14,CategoryId=2,Name_en= "Girls'", Name_ar="الفتيات" , Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-2ecb/k2-_d9bd550b-7a8a-4a4f-96c1-7f691907e7b1.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=15,CategoryId=2,Name_en= "Boys'", Name_ar= "الأولاد", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-4d52/k2-_e53ee8d4-7c89-4c43-b74a-f66096bb5d82.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=16,CategoryId=2,Name_en="Baby & Toddler" , Name_ar="الاطفال" , Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-2eef/k2-_ac29d4e9-e041-4998-a313-db0cd15136e0.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=17,CategoryId=2,Name_en="Shoes" , Name_ar= "أحذية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-f5e9/k2-_9966f2cf-edfa-48d3-bd78-1cfa14e4baa4.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=18,CategoryId=2,Name_en="Jewelry & Watches" , Name_ar= "المجوهرات والساعات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8d2e/k2-_c7aa356f-9d44-4d52-8311-b915b8668d4c.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=19,CategoryId=2,Name_en="Bags & accessories" , Name_ar= "الحقائب والإكسسوارات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-2306/k2-_8ddb1fe4-8867-42d0-8807-8f5074bc2d70.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Home
                new SubCategory() { Id=20,CategoryId=3,Name_en= "Kitchen & dining", Name_ar= "المطبخ وتناول الطعام", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-71c0/k2-_d18f8241-d0c9-4e3b-a18e-98a0a4c7444a.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=22,CategoryId=3,Name_en= "Furniture", Name_ar= "أثاث", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-3b37/k2-_e8b126fd-7e4d-445e-9818-931b74c2128a.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=23,CategoryId=3,Name_en= "Decor", Name_ar= "ديكور", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-aacc/k2-_4579d38f-ecb5-448c-a62a-bf806c2c694c.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=24,CategoryId=3,Name_en= "Bedding", Name_ar= "خاص بالفراش", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-9214/k2-_287ead61-7b87-4a36-8362-b669aafa1897.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=25,CategoryId=3,Name_en= "Storage", Name_ar= "خاص بالتخزين", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-1901/k2-_b6064017-0842-429c-b0be-4271e4f08cc0.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=26,CategoryId=3,Name_en= "Bath", Name_ar= "اغراض الحمامت ", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-400a/k2-_1cad0807-90ca-489f-871f-9e85756615c2.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=27,CategoryId=3,Name_en= "Appliances & floor care", Name_ar= "الأجهزة والعناية بالأرضيات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-2a49/k2-_465226e9-563c-4f85-8dd1-d46f472c45ba.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=28,CategoryId=3,Name_en= "Mattresses", Name_ar="المراتب" , Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8f79/k2-_781f3943-bbe9-46b8-9119-0b8359eaaf10.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=29,CategoryId=3,Name_en= "Kitchen appliances", Name_ar= "أدوات المطبخ", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-769c/k2-_41a8182e-c16e-44d7-94e1-8e7d137e27e2.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=30,CategoryId=3,Name_en= "Rugs", Name_ar="السجاد" , Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-2b54/k2-_98242d71-1929-4fd2-969c-8627090db99d.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=31,CategoryId=3,Name_en= "Arts & crafts", Name_ar= "الفنون والحرف", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-eead/k2-_0f2fb550-6fa1-4eac-9aa9-92369229cc97.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Patio & Garden
                new SubCategory() { Id=32,CategoryId=4,Name_en= "Grills", Name_ar= "مشاوي", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-36ae/k2-_4c3eb6c4-3e78-40a3-b33e-18f2bd20c89e.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=33,CategoryId=4,Name_en= "Outdoor power equipment", Name_ar= "معدات الطاقة في الهواء الطلق", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-bd96/k2-_bd449c8f-60f9-4840-be4e-acaea58d4bec.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=34,CategoryId=4,Name_en= "Garden Center", Name_ar= "مركز الحديقة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-706c/k2-_65ed6942-bc18-4822-b10a-d62a99079070.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=35,CategoryId=4,Name_en= "Live plants", Name_ar= "النباتات الحية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-48ef/k2-_d77f4308-adb4-4c4f-abbd-a47d4afdb222.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=36,CategoryId=4,Name_en= "Outdoor lighting", Name_ar= "إضاءة خارجية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-a8f7/k2-_4f08738d-6bf5-480e-a495-39815c4130a9.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=37,CategoryId=4,Name_en= "Outdoor heating", Name_ar= "تدفئة خارجية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-db2a/k2-_32a52379-4f15-46da-9fdd-9ede1cad444d.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Toys
                new SubCategory() { Id=38,CategoryId=5,Name_en= "Dolls & dollhouses", Name_ar= "الدمى وبيوت الدمى", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-7445/k2-_e0bc5732-a987-4696-8a64-2c6e716c8227.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=39,CategoryId=5,Name_en= "Action figures", Name_ar= "شخصيات العمل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-e01b/k2-_ae550749-f064-4c3a-b206-36ab115757b7.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=40,CategoryId=5,Name_en= "Building sets & blocks", Name_ar= "مجموعات البناء والكتل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-3f80/k2-_30ac46e8-3d47-40f2-94ac-cca57823ec4b.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=41,CategoryId=5,Name_en= "Bikes & ride ons", Name_ar= "دراجات وعربات الركوب", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-ad17/k2-_61d65755-3d0d-4c74-b5fc-4a88529bc281.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=42,CategoryId=5,Name_en= "RC toys & vehicles", Name_ar= "ألعاب ومركبات RC", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-ad17/k2-_61d65755-3d0d-4c74-b5fc-4a88529bc281.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=43,CategoryId=5,Name_en= "Plush", Name_ar= "قطيفة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8c92/k2-_e7eaccfb-330c-45a6-8faf-ae03d171996c.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=44,CategoryId=5,Name_en= "Games & puzzles", Name_ar= "الألعاب والألغاز", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-b31c/k2-_c2791383-f1e0-48aa-93f5-486d4ca82b0c.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=45,CategoryId=5,Name_en= "Minis & Surprise toys", Name_ar= "ألعاب صغيرة ومفاجأة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-90a7/k2-_8636c7ac-ced0-4153-b684-d106ebb7cb9a.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=46,CategoryId=5,Name_en= "Learning toys", Name_ar= "ألعاب التعلم", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-688a/k2-_12e28f5a-09a0-4416-ba66-a89f54f129cc.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=47,CategoryId=5,Name_en= "Arts & crafts", Name_ar= "الفنون والحرف", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-3301/k2-_279b1b8c-fbc0-46fb-9099-f691092aceb3.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Grocery
                new SubCategory() { Id=48,CategoryId=6,Name_en= "Pantry", Name_ar= "مخزن", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-175b/k2-_c68281b8-7783-4921-83b4-9600a029fdee.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=49,CategoryId=6,Name_en= "Frozen", Name_ar= "المجمدة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-3cd1/k2-_095cf6b1-09b8-42a2-8437-12bcefb3d061.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=50,CategoryId=6,Name_en= "Snacks", Name_ar= "وجبات خفيفة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-69b7/k2-_1316f031-c893-4f27-86fd-d04e1eab1699.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=51,CategoryId=6,Name_en= "Beverages", Name_ar= "المشروبات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-a37e/k2-_f1dc2ff7-4e09-49f9-86af-39bff13ae2fe.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=52,CategoryId=6,Name_en= "Breakfast & cereal", Name_ar= "الإفطار والحبوب", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-db9b/k2-_8c362cb4-5617-4b6d-97ba-a5d5e9a681e6.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=53,CategoryId=6,Name_en= "Coffee", Name_ar= "قهوة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-84d1/k2-_b0879d01-ed22-431a-9a18-9abe855b00aa.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=54,CategoryId=6,Name_en= "Meat & Seafood", Name_ar= "اللحوم والمأكولات البحرية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-1092/k2-_3e369461-bbdd-4959-af5b-002c5d6b3189.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=55,CategoryId=6,Name_en= "Produce", Name_ar= "ينتج", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-f12b/k2-_87b15a7e-ac14-4e42-a23a-44fd79998719.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=56,CategoryId=6,Name_en= "Deli", Name_ar= "أطعمة لذيذة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-dc72/k2-_05a696fc-4dcc-487b-b09a-798c36754e4f.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=57,CategoryId=6,Name_en= "Bread & Bakery", Name_ar= "الخبز والمخبز", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-a967/k2-_c83d7d82-81ec-4856-908c-7216e1b0db97.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=58,CategoryId=6,Name_en= "Dairy & Eggs", Name_ar= "الألبان والبيض", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-1016/k2-_cf0b0213-9e3e-4b9f-bf97-6e3827d5082f.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=59,CategoryId=6,Name_en= "Holiday mains", Name_ar= "أنابيب عطلة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-1092/k2-_3e369461-bbdd-4959-af5b-002c5d6b3189.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Personal Care
                new SubCategory() { Id=60,CategoryId=7,Name_en= "Oral care", Name_ar= "العناية بالفم", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-c0ca/k2-_28bfc095-8d80-48ee-a2d4-c606a32a9d97.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=61,CategoryId=7,Name_en= "Shave", Name_ar= "حلاقة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-fbc4/k2-_e9f73924-d5ec-4925-a014-7d774dba6bee.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=62,CategoryId=7,Name_en= "Bath & body", Name_ar= "الحمام والجسم", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8e18/k2-_4d5f8c8e-8aaa-4538-bbaa-f9f95ecb9759.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=63,CategoryId=7,Name_en= "Men’s grooming", Name_ar= "العناية بالرجال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-f10e/k2-_d2e8179e-510a-4b65-8d5a-3b7b9d3bd760.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=64,CategoryId=7,Name_en= "Feminine care", Name_ar= "رعاية أنثوية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-35ce/k2-_fbe7e1bd-32d0-45ca-9d33-5b9b4ad751a3.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=65,CategoryId=7,Name_en= "Hand soaps & sanitizers", Name_ar= "صابون ومعقمات اليد", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-d3ba/k2-_20c5c04f-c402-4972-840b-454d3792955f.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=66,CategoryId=7,Name_en= "Sexual wellness", Name_ar= "العافية الجنسية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-ee2e/k2-_a93478a3-e4cf-4340-91f0-cfe315138a04.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=67,CategoryId=7,Name_en= "Incontinence", Name_ar= "سلس البول", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-e066/k2-_d0055f60-c92d-4411-9526-9bb244cf94fc.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=68,CategoryId=7,Name_en= "Equate", Name_ar= "ايكوات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-9423/k2-_78e57133-611c-4a25-890a-c7dd12f60aeb.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=69,CategoryId=7,Name_en= "Cotton essentials", Name_ar= "أساسيات القطن", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-2791/k2-_8e0551fe-af9f-4b6a-a275-552e57fa03c9.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=70,CategoryId=7,Name_en= "Deodorants & antiperspirants", Name_ar= "مزيلات الروائح ومضادات التعرق", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-aae8/k2-_d74de6af-642a-4438-a030-764054709ffe.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                new SubCategory() { Id=71,CategoryId=7,Name_en= "Minis & travel sizes", Name_ar= "أحجام صغيرة ومناسبة للسفر", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-1f43/k2-_5404c80e-c8b3-49df-b408-b8be79d09ebb.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Beauty
                new SubCategory() { Id=72,CategoryId=8,Name_en= "Haircare", Name_ar= "العناية بالشعر", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-1bc7/k2-_0d4657be-3345-4ed2-9543-ae1ae1052e7f.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=73,CategoryId=8,Name_en= "Skincare", Name_ar= "العناية بالبشرة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8b58/k2-_ce998366-7abc-40fc-8ff6-4c2099ac27bc.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=74,CategoryId=8,Name_en= "Makeup", Name_ar= "ماكياج", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-3aa2/k2-_b564ac6f-8cfb-4194-93a3-707fd5425d48.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=75,CategoryId=8,Name_en= "Nail care", Name_ar= "العناية بالأظافر", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-7154/k2-_322f88f8-0ae8-4d96-a28b-989c80862707.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=76,CategoryId=8,Name_en= "Hair tools", Name_ar= "أدوات الشعر", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-bd90/k2-_0625dc36-c17d-47bd-a560-5c7cc9e93328.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=77,CategoryId=8,Name_en= "Fragrances", Name_ar= "العطور", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-ff71/k2-_fa651d8b-4f85-469a-894f-557147a784cb.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=78,CategoryId=8,Name_en= "Premium beauty", Name_ar= "جمال ممتاز", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-2ba2/k2-_070d9fe3-8609-49e3-9d31-ce3e6a75e015.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=79,CategoryId=8,Name_en= "BEAUTYSPACE", Name_ar= "بيوتي سبيس", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8a70/k2-_c36f6a6d-2720-42c8-96fb-cbad37eb7d24.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=80,CategoryId=8,Name_en= "Beauty tech & tools", Name_ar= "تكنولوجيا وأدوات التجميل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-1adb/k2-_b6bca9fc-addb-4c10-8806-dda4058e7769.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=81,CategoryId=8,Name_en= "Suncare", Name_ar= "العناية من الشمس", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8079/k2-_6031819b-904d-4884-a612-aed227aa87f4.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=82,CategoryId=8,Name_en= "Curly hair & more", Name_ar= "شعر مجعد وأكثر", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-5a40/k2-_8c078ecd-cffe-4f00-9f6b-31fce2d321bf.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=83,CategoryId=8,Name_en= "Men’s grooming", Name_ar= "العناية بالرجال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-d580/k2-_29cb8e1b-03c0-4fb7-ae2b-c308555d2a34.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Health & wellness
                new SubCategory() { Id=84,CategoryId=9,Name_en= "Cold and flu medicine", Name_ar= "دواء البرد والانفلونزا", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-c7ef/k2-_c37dd76a-55b9-4ae0-8829-960d033ea6de.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=85,CategoryId=9,Name_en= "Allergy", Name_ar= "حساسية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-43ca/k2-_b01384a0-4148-44ae-813a-0b84b95ee51d.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=86,CategoryId=9,Name_en= "Pain management", Name_ar= "إدارة الألم", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-f3a7/k2-_14b90bd1-198c-4ec1-8600-bedab6cd0178.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=87,CategoryId=9,Name_en= "Home health tests", Name_ar= "اختبارات الصحة المنزلية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-dea9/k2-_f5b79a63-df62-4f59-ac69-e01ef8a00703.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=88,CategoryId=9,Name_en= "First aid", Name_ar= "الإسعافات الأولية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-de9b/k2-_c549d0be-a05b-456c-a0ce-772374257169.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=89,CategoryId=9,Name_en= "Children's medicine", Name_ar= "طب الاطفال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-1de0/k2-_9fbcdf03-f013-4a47-ba3c-28aa6b148a0b.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=90,CategoryId=9,Name_en= "Nutrition and weight management", Name_ar= "التغذية وإدارة الوزن", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-e7ac/k2-_fabf9860-5ab3-485b-bc84-c9fee5183d1a.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=91,CategoryId=9,Name_en= "Vitamins and supplements", Name_ar= "الفيتامينات والمكملات الغذائية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-3940/k2-_22fa0ad9-3314-42c3-aa9d-70b198423682.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=92,CategoryId=9,Name_en= "Digestive health", Name_ar= "صحة الجهاز الهضمي", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-97d8/k2-_4248abc0-0f56-4fae-9fde-d255ae8e408f.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=93,CategoryId=9,Name_en= "Protein shakes", Name_ar= "يهز البروتين", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-f5d0/k2-_7677cbed-211e-4215-9ff4-4e3fdff6db96.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=94,CategoryId=9,Name_en= "Sleep support", Name_ar= "دعم النوم", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-b6c1/k2-_e3c511b5-977b-4546-9667-bd07f6c80a6d.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=95,CategoryId=9,Name_en= "Women’s health", Name_ar= "صحة المرأة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-d26f/k2-_15bb4d84-1f32-4bcf-a73e-a8344d36081e.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=96,CategoryId=9,Name_en= "Immunity system boosters", Name_ar= "مقويات لجهاز المناعة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-9e87/k2-_188e2c5e-563d-4222-a074-c5b38014c257.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=97,CategoryId=9,Name_en= "Mobility aids", Name_ar= "مساعدات التنقل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-ce3b/k2-_07990a2b-8162-4b4a-9096-6bd31e77459c.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=98,CategoryId=9,Name_en= "Massage", Name_ar= "تدليك", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-d2ca/k2-_13ba5685-5145-4dfb-beee-bcea86e99ac5.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=99,CategoryId=9,Name_en= "Diabetes", Name_ar= "السكري", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-3aab/k2-_e7b2ff07-5849-4b65-987e-7ccbf0c1bf31.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=100,CategoryId=9,Name_en= "Heating pads", Name_ar= "منصات التدفئة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-42c4/k2-_04597912-e0fa-45c2-9fc5-f3d8364d6d10.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=101,CategoryId=9,Name_en= "OTC hearing aids", Name_ar= "المعينات السمعية المتاحة دون وصفة طبية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-5948/k2-_476eda52-9485-498e-8526-f3cd314d031e.v1.png?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Baby
                new SubCategory() { Id=102,CategoryId=10,Name_en= "Car seats", Name_ar= "مقاعد السيارة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-96fb/k2-_b0574b12-e225-427a-bb51-a5edfd26805e.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=103,CategoryId=10,Name_en= "Strollers", Name_ar= "عربات الأطفال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-c5c9/k2-_8dba7cd7-a98c-4abc-8d86-4a7cd1d91a88.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=104,CategoryId=10,Name_en= "Baby gear", Name_ar= "معدات اطفال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-5265/k2-_4e7cd1a5-bc1d-43c1-b1bf-c656ac4fedc4.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=105,CategoryId=10,Name_en= "Infant activity", Name_ar= "نشاط الرضع", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-c9e1/k2-_16c9d15b-04c1-4d8e-a49d-cfcc8ced87a5.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=106,CategoryId=10,Name_en= "Nursery & decor", Name_ar= "الحضانة والديكور", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-76ad/k2-_c5868220-99fa-4ac5-b2b6-bcda76bbcfc1.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=107,CategoryId=10,Name_en= "Toddler room", Name_ar= "غرفة طفل صغير", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-5523/k2-_0992b3b1-5805-4522-86dd-f20763952726.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=108,CategoryId=10,Name_en= "Health & safety", Name_ar= "الصحة والسلامة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-6947/k2-_f6a1dd0f-9469-40be-953b-1294e1defbc1.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=109,CategoryId=10,Name_en= "Diapers & wipes", Name_ar= "حفاضات ومناديل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-d765/k2-_2fe91f82-26e7-4a30-a961-14796e8b6acb.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=110,CategoryId=10,Name_en= "Nursing & feeding", Name_ar= "التمريض والتغذية", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-ac6d/k2-_9901e4b3-1c5e-4ca6-bb24-95e1052f4474.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=111,CategoryId=10,Name_en= "Bath & potty", Name_ar= "حمام ونونية الأطفال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-22e7/k2-_5ee8911c-f9f8-47bb-90fc-63935754e219.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=112,CategoryId=10,Name_en= "Baby skincare", Name_ar= "العناية ببشرة الطفل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-995f/k2-_2fac2b0d-e6d6-4c26-89a0-062aa8e6d9e9.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=113,CategoryId=10,Name_en= "Baby toys", Name_ar= "العاب اطفال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-4980/k2-_3b2fee35-f128-4b25-8d96-72ccaf53abea.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=114,CategoryId=10,Name_en= "Baby apparel", Name_ar= "ملابس اطفال", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-de71/k2-_daac1bd2-d7a0-4541-81e8-7f84a8dd9d93.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=115,CategoryId=10,Name_en= "Toddler apparel", Name_ar= "ملابس طفل صغير", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-c1d2/k2-_4c39fc50-060b-4747-9c0c-7c2b2740ab1a.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=116,CategoryId=10,Name_en= "Maternity clothing", Name_ar= "ملابس الأمومة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-385d/k2-_ddea79f3-d482-4171-94ce-f2a66090b4a7.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=117,CategoryId=10,Name_en= "Prenatal needs", Name_ar= "احتياجات ما قبل الولادة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-a9cc/k2-_9dee8109-78f2-4e2a-8e45-08c2b313aefe.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=118,CategoryId=10,Name_en= "Postpartum needs", Name_ar= "احتياجات ما بعد الولادة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-9975/k2-_75522f41-cd78-4dbd-be4f-2916c875a285.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=119,CategoryId=10,Name_en= "Newborn essentials", Name_ar= "أساسيات حديثي الولادة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-d583/k2-_ff2ae8ff-a0e0-4d7a-b39f-d7de0b0ed012.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Auto & tires
                new SubCategory() { Id=121,CategoryId=11,Name_en= "Batteries & accessories", Name_ar= "البطاريات وملحقاتها", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-6620/k2-_ce064716-c62f-458d-b0f3-c1443fee52ab.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=122,CategoryId=11,Name_en= "Tires", Name_ar= "الإطارات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-8eb7/k2-_a300c67f-95bc-4250-b869-f820319c3ca8.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=123,CategoryId=11,Name_en= "Oil & fluids", Name_ar= "الزيوت والسوائل", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-87ef/k2-_f08986e4-9b07-4820-9fc3-ae88e185bf8a.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=124,CategoryId=11,Name_en= "Detailing & car care", Name_ar= "التفاصيل والعناية بالسيارات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-3a77/k2-_9a3f0519-5e99-42af-ab56-18e65d697ba2.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=125,CategoryId=11,Name_en= "Auto parts ", Name_ar= "قطع غيار السيارات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-c1e2/k2-_09646e1d-2c80-4048-b26c-808eccbbbf7f.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=126,CategoryId=11,Name_en= "Auto electronics", Name_ar= "إلكترونيات السيارات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-6ef9/k2-_0ca74f92-ca5d-408e-a0ea-86149bdfce17.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" },
                //Home Improvement
                new SubCategory() { Id=127,CategoryId=12,Name_en= "Heating", Name_ar= "التدفئة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-5c02/k2-_2d271e01-70a7-4563-a9f2-049706a75e04.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=128,CategoryId=12,Name_en= "Humidifiers", Name_ar= "مرطبات", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-66b6/k2-_007af523-27da-49b5-9306-21a7017ddb87.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=129,CategoryId=12,Name_en= "Garage storage", Name_ar= "تخزين المرآب", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-38dd/k2-_f25cdd97-07b6-4abc-9664-05aaad1fe587.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=130,CategoryId=12,Name_en= "Plumbing", Name_ar= "السباكة", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-504c/k2-_4c574433-3065-40f6-8046-c2e281c1dd19.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=131,CategoryId=12,Name_en= "Kitchen renovation", Name_ar= "تجديد المطبخ", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-5103/k2-_1222f577-d893-4c88-98b1-a8a2f4dc8f0c.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" }, 
                new SubCategory() { Id=132,CategoryId=12,Name_en= "Water filtration", Name_ar= "ترشيح المياه", Image = "https://i5.walmartimages.com/dfw/4ff9c6c9-897c/k2-_54304291-1c14-435b-aba2-f6c924dec9fe.v1.jpg?odnHeight=290&odnWidth=290&odnBg=FFFFFF" } 
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
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
