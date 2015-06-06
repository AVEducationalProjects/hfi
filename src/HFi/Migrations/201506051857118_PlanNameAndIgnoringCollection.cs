namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanNameAndIgnoringCollection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plans", "Name", c => c.String());
            AddColumn("dbo.PlanEntries", "PlanEntry_Id", c => c.Int());
            CreateIndex("dbo.PlanEntries", "PlanEntry_Id");
            AddForeignKey("dbo.PlanEntries", "PlanEntry_Id", "dbo.PlanEntries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanEntries", "PlanEntry_Id", "dbo.PlanEntries");
            DropIndex("dbo.PlanEntries", new[] { "PlanEntry_Id" });
            DropColumn("dbo.PlanEntries", "PlanEntry_Id");
            DropColumn("dbo.Plans", "Name");
        }
    }
}
