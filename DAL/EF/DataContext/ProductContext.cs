using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using System.Data.Entity;

namespace DAL.DataContext
{
    public class ProductContext:DbContext
    {

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Provider> Providers { get; set; }


        static ProductContext()
        {
            Database.SetInitializer<ProductContext>(new StoreInitializer());
        }
        public ProductContext()
        {

        }
        public ProductContext(string connectionString):base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provider>().HasMany(c => c.Products)
                .WithMany(s => s.Providers)
                .Map(t => t.MapLeftKey("Provider_Id")
                .MapRightKey("Product_Id")
                .ToTable("ProviderProducts"));
        }

    }
    
    public class StoreInitializer: DropCreateDatabaseIfModelChanges<ProductContext>
    {

        protected override void Seed(ProductContext db)
        {
            Category notebooks = new Category { Name="Notebooks" };
            Category phones = new Category { Name = "Phones" };
            Category accessories = new Category { Name = "Accessories" };
            Category tablets = new Category { Name = "Tablets" };
            Category ssdDisks = new Category { Name = "SSD" };


            db.Categories.Add(notebooks);
            db.Categories.Add(phones);
            db.Categories.Add(accessories);
            db.Categories.Add(tablets);
            db.Categories.Add(ssdDisks);
            db.SaveChanges();

            Product notebook1 = new Product { Name = "Prestigio SmartBook 141C", Price=5699 , Category=notebooks };
            Product notebook2 = new Product { Name = "Acer Extensa EX2519-C313 ", Price = 6699, Category = notebooks };
            Product notebook3 = new Product { Name = "Lenovo IdeaPad 320-15IKB ", Price = 15699, Category = notebooks };
            Product notebook4 = new Product { Name = "Asus VivoBook Max X541NA ", Price = 7199, Category = notebooks };
            Product notebook5 = new Product { Name = "Dell Inspiron 3552 ", Price = 6773, Category = notebooks };


            Product phone1 = new Product { Name = "Galaxy S8 64GB Midnight Black", Price = 21999, Category = phones };
            Product phone2 = new Product { Name = "Lenovo K6 (K33a48) Silver", Price = 3229, Category = phones };
            Product phone3 = new Product { Name = "Galaxy A8 2018 32GB Black ", Price = 14999, Category = phones };
            Product phone4 = new Product { Name = "Huawei P8 Lite 2017 (PRA-LA1)", Price = 4999, Category = phones };
            Product phone5 = new Product { Name = "Samsung Galaxy J5 (2017) J530 Black", Price = 6799, Category = phones };


            Product accessory1 = new Product { Name = "Black Brier", Price=279, Category=accessories };
            Product accessory2 = new Product { Name = "Pocket Book", Price = 368, Category = accessories };
            Product accessory3 = new Product { Name = "Air OnPocket", Price = 494, Category = accessories };
            Product accessory4 = new Product { Name = "ARS Retro", Price = 270, Category = accessories };
            Product accessory5 = new Product { Name = "AirOn Premium", Price = 159, Category = accessories };

            Product tablet1 = new Product { Name = "Huawei MediaPad T3", Price = 2299, Category = tablets };
            Product tablet2 = new Product { Name = "Pixus Vision 10.1 3G 3/16GB", Price = 4499, Category = tablets };
            Product tablet3 = new Product { Name = "Samsung Galaxy Tab E 9.6", Price = 5299, Category = tablets };
            Product tablet4 = new Product { Name = "Apple iPad A1822 Wi-Fi 32GB Silver", Price = 10499, Category = tablets };
            Product tablet5 = new Product { Name = "Lenovo Tab 4 10 LTE 32GB Slate", Price = 6299, Category = tablets };


            Product ssdDisk1 = new Product { Name = "ADATA Premier SP600 64GB 2.5", Price = 1045, Category = ssdDisks };
            Product ssdDisk2 = new Product { Name = "Kingston SSDNow A400", Price = 1199, Category = ssdDisks };
            Product ssdDisk3 = new Product { Name = "Samsung 960 Evo series", Price = 7299, Category = ssdDisks };
            Product ssdDisk4 = new Product { Name = "Transcend SSD220S", Price = 2034, Category = ssdDisks };
            Product ssdDisk5 = new Product { Name = "SanDisk SSD Plus", Price = 1251, Category = ssdDisks };

            db.Products.AddRange(new List<Product> { notebook1,notebook2,notebook3,notebook4,notebook5,
                                                     phone1,phone2,phone3,phone4,phone5,
                                                     accessory1,accessory2,accessory3,accessory4,accessory5,
                                                     tablet1,tablet2,tablet3,tablet4,tablet5,
                                                     ssdDisk1,ssdDisk2,ssdDisk3,ssdDisk4,ssdDisk5});

            db.SaveChanges();


            Provider provider1 = new Provider { Name = "Comfy", Adress = "Kiev", Products = new List<Product> { tablet1, tablet2, notebook1, notebook2, accessory1, accessory2 } };
            Provider provider2 = new Provider { Name = "Rozetka", Adress = "Lvov", Products = new List<Product> { tablet1, tablet3,tablet4, notebook1, notebook3,notebook5, accessory1, accessory4,accessory5 } };
            Provider provider3 = new Provider { Name = "Tech", Adress = "Odessa", Products = new List<Product> { tablet4, tablet5, notebook2, notebook3,notebook4, accessory2, accessory3,accessory5 } };

            db.Providers.AddRange(new List<Provider> { provider1, provider2, provider3 });
        }


    }
}
