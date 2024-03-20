namespace e_commerce_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DelivaryName", c => c.String());
            AddColumn("dbo.Orders", "Days", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Days");
            DropColumn("dbo.Orders", "DelivaryName");
        }
    }
}
