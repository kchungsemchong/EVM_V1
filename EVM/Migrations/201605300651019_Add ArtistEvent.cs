namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddArtistEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistEvents",
                c => new
                    {
                        ArtistId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArtistId, t.EventId });
        }

        public override void Down()
        {
            DropTable("dbo.ArtistEvents");
        }
    }
}