namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverhaulC : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "IsInStock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "IsInStock", c => c.Boolean(nullable: false));
        }
    }
}
