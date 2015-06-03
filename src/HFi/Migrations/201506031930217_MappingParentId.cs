namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MappingParentId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Categories", name: "Parent_Id", newName: "ParentId");
            RenameIndex(table: "dbo.Categories", name: "IX_Parent_Id", newName: "IX_ParentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Categories", name: "IX_ParentId", newName: "IX_Parent_Id");
            RenameColumn(table: "dbo.Categories", name: "ParentId", newName: "Parent_Id");
        }
    }
}
