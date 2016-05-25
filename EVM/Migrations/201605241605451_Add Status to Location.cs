namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatustoLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "Status");
        }
    }
}
