namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArtist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DtAdded = c.DateTime(),
                        FacebookUrl = c.String(),
                    })
                .PrimaryKey(t => t.ArtistId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Artists");
        }
    }
}
