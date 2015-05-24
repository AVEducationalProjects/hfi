namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Source = c.String(),
                        Purpose = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SourceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ABitAmountTerm_A1 = c.Int(nullable: false),
                        ABitAmountTerm_A2 = c.Int(nullable: false),
                        ABitAmountTerm_A3 = c.Int(nullable: false),
                        ABitAmountTerm_A4 = c.Int(nullable: false),
                        SmallAmountTerm_A1 = c.Int(nullable: false),
                        SmallAmountTerm_A2 = c.Int(nullable: false),
                        SmallAmountTerm_A3 = c.Int(nullable: false),
                        SmallAmountTerm_A4 = c.Int(nullable: false),
                        NormalAmountTerm_A1 = c.Int(nullable: false),
                        NormalAmountTerm_A2 = c.Int(nullable: false),
                        NormalAmountTerm_A3 = c.Int(nullable: false),
                        NormalAmountTerm_A4 = c.Int(nullable: false),
                        LargeAmountTerm_A1 = c.Int(nullable: false),
                        LargeAmountTerm_A2 = c.Int(nullable: false),
                        LargeAmountTerm_A3 = c.Int(nullable: false),
                        LargeAmountTerm_A4 = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Category1_Id = c.Int(),
                        Category2_Id = c.Int(),
                        Category3_Id = c.Int(),
                        RootCategory_Id = c.Int(),
                        Source1_Id = c.Int(),
                        Source2_Id = c.Int(),
                        Source3_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SourceCategories", t => t.Category1_Id)
                .ForeignKey("dbo.SourceCategories", t => t.Category2_Id)
                .ForeignKey("dbo.SourceCategories", t => t.Category3_Id)
                .ForeignKey("dbo.Categories", t => t.RootCategory_Id)
                .ForeignKey("dbo.Sources", t => t.Source1_Id)
                .ForeignKey("dbo.Sources", t => t.Source2_Id)
                .ForeignKey("dbo.Sources", t => t.Source3_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Category1_Id)
                .Index(t => t.Category2_Id)
                .Index(t => t.Category3_Id)
                .Index(t => t.RootCategory_Id)
                .Index(t => t.Source1_Id)
                .Index(t => t.Source2_Id)
                .Index(t => t.Source3_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        LastChanged = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.PlanEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_Id = c.Int(),
                        Plan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Plans", t => t.Plan_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Plan_Id);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Proposition = c.String(),
                        Conclusion_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Conclusion_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Conclusion_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Source3_Id", "dbo.Sources");
            DropForeignKey("dbo.AspNetUsers", "Source2_Id", "dbo.Sources");
            DropForeignKey("dbo.AspNetUsers", "Source1_Id", "dbo.Sources");
            DropForeignKey("dbo.Rules", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rules", "Conclusion_Id", "dbo.Categories");
            DropForeignKey("dbo.AspNetUsers", "RootCategory_Id", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Plans", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PlanEntries", "Plan_Id", "dbo.Plans");
            DropForeignKey("dbo.PlanEntries", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Category3_Id", "dbo.SourceCategories");
            DropForeignKey("dbo.AspNetUsers", "Category2_Id", "dbo.SourceCategories");
            DropForeignKey("dbo.AspNetUsers", "Category1_Id", "dbo.SourceCategories");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Transactions", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
            DropIndex("dbo.Rules", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Rules", new[] { "Conclusion_Id" });
            DropIndex("dbo.PlanEntries", new[] { "Plan_Id" });
            DropIndex("dbo.PlanEntries", new[] { "Category_Id" });
            DropIndex("dbo.Plans", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Source3_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source2_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source1_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "RootCategory_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Category3_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Category2_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Category1_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Transactions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Transactions", new[] { "Category_Id" });
            DropIndex("dbo.Categories", new[] { "Parent_Id" });
            DropTable("dbo.Sources");
            DropTable("dbo.Rules");
            DropTable("dbo.PlanEntries");
            DropTable("dbo.Plans");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SourceCategories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Transactions");
            DropTable("dbo.Categories");
        }
    }
}
