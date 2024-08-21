namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dummy : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "We7daID" });
            CreateIndex("dbo.Users", "We7daID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "We7daID" });
            CreateIndex("dbo.Users", "We7daID", unique: true);
        }
    }
}
