namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTariffs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tariffs",
                c => new
                    {
                        TariffId = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TariffId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tariffs", "EventId", "dbo.Events");
            DropIndex("dbo.Tariffs", new[] { "EventId" });
            DropTable("dbo.Tariffs");
        }
    }
}
