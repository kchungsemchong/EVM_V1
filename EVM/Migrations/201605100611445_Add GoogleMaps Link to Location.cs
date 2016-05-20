namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGoogleMapsLinktoLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "GoogleMapsLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "GoogleMapsLink");
        }
    }
}
