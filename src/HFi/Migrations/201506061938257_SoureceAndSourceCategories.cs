namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SoureceAndSourceCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SourceCategoryToSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Source_Id = c.Int(),
                        SourceCategory_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .ForeignKey("dbo.SourceCategories", t => t.SourceCategory_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Source_Id)
                .Index(t => t.SourceCategory_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SourceCategoryToSources", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SourceCategoryToSources", "SourceCategory_Id", "dbo.SourceCategories");
            DropForeignKey("dbo.SourceCategoryToSources", "Source_Id", "dbo.Sources");
            DropIndex("dbo.SourceCategoryToSources", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.SourceCategoryToSources", new[] { "SourceCategory_Id" });
            DropIndex("dbo.SourceCategoryToSources", new[] { "Source_Id" });
            DropTable("dbo.SourceCategoryToSources");
            DropTable("dbo.Sources");
        }
    }
}
