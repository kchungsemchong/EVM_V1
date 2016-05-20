namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSponsor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sponsors",
                c => new
                    {
                        SponsorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DtAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.SponsorId);
            
            AddColumn("dbo.Artists", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Status");
            DropTable("dbo.Sponsors");
        }
    }
}
