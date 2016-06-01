namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoPropertiestoSponsor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sponsors", "ContentType", c => c.String());
            AddColumn("dbo.Sponsors", "Content", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sponsors", "Content");
            DropColumn("dbo.Sponsors", "ContentType");
        }
    }
}
