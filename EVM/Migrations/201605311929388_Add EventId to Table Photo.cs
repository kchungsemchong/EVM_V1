namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventIdtoTablePhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "EventId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "EventId");
        }
    }
}
