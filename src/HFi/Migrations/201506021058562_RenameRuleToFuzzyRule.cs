namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameRuleToFuzzyRule : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Rules", newName: "FuzzyRules");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FuzzyRules", newName: "Rules");
        }
    }
}
