namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Products",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Price = c.Double(nullable: false),
                    CategoryId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);

            CreateTable(
                "dbo.Providers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Adress = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ProviderProducts",
                c => new
                {
                    Provider_Id = c.Int(nullable: false),
                    Product_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Provider_Id, t.Product_Id })
                .ForeignKey("dbo.Providers", t => t.Provider_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Provider_Id)
                .Index(t => t.Product_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ProviderProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProviderProducts", "Provider_Id", "dbo.Providers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ProviderProducts", new[] { "Product_Id" });
            DropIndex("dbo.ProviderProducts", new[] { "Provider_Id" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.ProviderProducts");
            DropTable("dbo.Providers");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
