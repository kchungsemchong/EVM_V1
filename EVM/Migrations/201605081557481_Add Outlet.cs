namespace EVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOutlet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Outlets",
                c => new
                    {
                        OutletId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.OutletId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Outlets");
        }
    }
}
