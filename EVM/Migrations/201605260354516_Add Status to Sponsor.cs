namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatustoSponsor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sponsors", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sponsors", "Status");
        }
    }
}
