namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FardDetailsState : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FardDetails", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FardDetails", "Status", c => c.Int(nullable: false));
        }
    }
}
