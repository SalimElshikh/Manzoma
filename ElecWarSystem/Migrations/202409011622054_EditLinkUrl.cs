namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditLinkUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Manzomats", "LinkUrl", c => c.String());
            DropColumn("dbo.Manzomats", "ButtonLink");
            DropColumn("dbo.Manzomats", "ButtonClass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Manzomats", "ButtonClass", c => c.String());
            AddColumn("dbo.Manzomats", "ButtonLink", c => c.String());
            DropColumn("dbo.Manzomats", "LinkUrl");
        }
    }
}
