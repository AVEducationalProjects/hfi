namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBadRelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Category1_Id", "dbo.SourceCategories");
            DropForeignKey("dbo.AspNetUsers", "Category2_Id", "dbo.SourceCategories");
            DropForeignKey("dbo.AspNetUsers", "Category3_Id", "dbo.SourceCategories");
            DropForeignKey("dbo.AspNetUsers", "Source1_Id", "dbo.Sources");
            DropForeignKey("dbo.AspNetUsers", "Source2_Id", "dbo.Sources");
            DropForeignKey("dbo.AspNetUsers", "Source3_Id", "dbo.Sources");
            DropIndex("dbo.AspNetUsers", new[] { "Category1_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Category2_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Category3_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source1_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source2_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source3_Id" });
            DropColumn("dbo.AspNetUsers", "Category1_Id");
            DropColumn("dbo.AspNetUsers", "Category2_Id");
            DropColumn("dbo.AspNetUsers", "Category3_Id");
            DropColumn("dbo.AspNetUsers", "Source1_Id");
            DropColumn("dbo.AspNetUsers", "Source2_Id");
            DropColumn("dbo.AspNetUsers", "Source3_Id");
            DropTable("dbo.Sources");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Source3_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Source2_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Source1_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Category3_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Category2_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Category1_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Source3_Id");
            CreateIndex("dbo.AspNetUsers", "Source2_Id");
            CreateIndex("dbo.AspNetUsers", "Source1_Id");
            CreateIndex("dbo.AspNetUsers", "Category3_Id");
            CreateIndex("dbo.AspNetUsers", "Category2_Id");
            CreateIndex("dbo.AspNetUsers", "Category1_Id");
            AddForeignKey("dbo.AspNetUsers", "Source3_Id", "dbo.Sources", "Id");
            AddForeignKey("dbo.AspNetUsers", "Source2_Id", "dbo.Sources", "Id");
            AddForeignKey("dbo.AspNetUsers", "Source1_Id", "dbo.Sources", "Id");
            AddForeignKey("dbo.AspNetUsers", "Category3_Id", "dbo.SourceCategories", "Id");
            AddForeignKey("dbo.AspNetUsers", "Category2_Id", "dbo.SourceCategories", "Id");
            AddForeignKey("dbo.AspNetUsers", "Category1_Id", "dbo.SourceCategories", "Id");
        }
    }
}
