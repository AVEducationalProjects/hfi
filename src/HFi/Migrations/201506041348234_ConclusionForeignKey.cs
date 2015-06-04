namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConclusionForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FuzzyRules", "Conclusion_Id", "dbo.Categories");
            DropIndex("dbo.FuzzyRules", new[] { "Conclusion_Id" });
            RenameColumn(table: "dbo.FuzzyRules", name: "Conclusion_Id", newName: "ConclusionId");
            AlterColumn("dbo.FuzzyRules", "ConclusionId", c => c.Int(nullable: false));
            CreateIndex("dbo.FuzzyRules", "ConclusionId");
            AddForeignKey("dbo.FuzzyRules", "ConclusionId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FuzzyRules", "ConclusionId", "dbo.Categories");
            DropIndex("dbo.FuzzyRules", new[] { "ConclusionId" });
            AlterColumn("dbo.FuzzyRules", "ConclusionId", c => c.Int());
            RenameColumn(table: "dbo.FuzzyRules", name: "ConclusionId", newName: "Conclusion_Id");
            CreateIndex("dbo.FuzzyRules", "Conclusion_Id");
            AddForeignKey("dbo.FuzzyRules", "Conclusion_Id", "dbo.Categories", "Id");
        }
    }
}
