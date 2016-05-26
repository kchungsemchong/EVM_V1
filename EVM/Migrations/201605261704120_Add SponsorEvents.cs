namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSponsorEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SponsorEvents",
                c => new
                    {
                        SponsorId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SponsorId, t.EventId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SponsorEvents");
        }
    }
}
