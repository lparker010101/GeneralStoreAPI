namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverhaulA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "LastName", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "FullName", c => c.String());
            DropColumn("dbo.Customers", "LastName");
        }
    }
}
