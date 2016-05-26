namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusStreetCitytoOutlet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Outlets", "Status", c => c.String());
            AddColumn("dbo.Outlets", "Street", c => c.String(nullable: false));
            AddColumn("dbo.Outlets", "City", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Outlets", "City");
            DropColumn("dbo.Outlets", "Street");
            DropColumn("dbo.Outlets", "Status");
        }
    }
}
