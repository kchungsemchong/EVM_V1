namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        LocationId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        EventDate = c.DateTime(),
                        DtAdded = c.DateTime(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "LocationId", "dbo.Locations");
            DropIndex("dbo.Events", new[] { "LocationId" });
            DropTable("dbo.Events");
        }
    }
}
