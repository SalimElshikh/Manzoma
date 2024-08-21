namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeSended : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tmam", "Sa3at", c => c.String());
            DropColumn("dbo.Tmam", "Connectivity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tmam", "Connectivity", c => c.Byte(nullable: false));
            DropColumn("dbo.Tmam", "Sa3at");
        }
    }
}
