namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanDeletionCascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanEntries", "Plan_Id", "dbo.Plans");
            DropIndex("dbo.PlanEntries", new[] { "Plan_Id" });
            AlterColumn("dbo.PlanEntries", "Plan_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.PlanEntries", "Plan_Id");
            AddForeignKey("dbo.PlanEntries", "Plan_Id", "dbo.Plans", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanEntries", "Plan_Id", "dbo.Plans");
            DropIndex("dbo.PlanEntries", new[] { "Plan_Id" });
            AlterColumn("dbo.PlanEntries", "Plan_Id", c => c.Int());
            CreateIndex("dbo.PlanEntries", "Plan_Id");
            AddForeignKey("dbo.PlanEntries", "Plan_Id", "dbo.Plans", "Id");
        }
    }
}
