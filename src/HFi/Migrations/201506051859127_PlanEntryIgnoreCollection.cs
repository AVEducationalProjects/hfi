namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanEntryIgnoreCollection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanEntries", "PlanEntry_Id", "dbo.PlanEntries");
            DropIndex("dbo.PlanEntries", new[] { "PlanEntry_Id" });
            DropColumn("dbo.PlanEntries", "PlanEntry_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanEntries", "PlanEntry_Id", c => c.Int());
            CreateIndex("dbo.PlanEntries", "PlanEntry_Id");
            AddForeignKey("dbo.PlanEntries", "PlanEntry_Id", "dbo.PlanEntries", "Id");
        }
    }
}
