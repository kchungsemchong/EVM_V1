namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToTariff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tariffs", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tariffs", "Status");
        }
    }
}
