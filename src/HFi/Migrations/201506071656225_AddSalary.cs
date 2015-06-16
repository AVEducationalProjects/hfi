namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSalary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Salary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Salary");
        }
    }
}
