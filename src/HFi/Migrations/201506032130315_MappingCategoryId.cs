namespace HFi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MappingCategoryId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Transactions", name: "Category_Id", newName: "CategoryId");
            RenameIndex(table: "dbo.Transactions", name: "IX_Category_Id", newName: "IX_CategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Transactions", name: "IX_CategoryId", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Transactions", name: "CategoryId", newName: "Category_Id");
        }
    }
}
