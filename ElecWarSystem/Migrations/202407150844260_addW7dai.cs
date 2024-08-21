namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addW7dai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KharegTmarkoz", "We7daRa2eeseya_ID", c => c.Int());
            CreateIndex("dbo.KharegTmarkoz", "We7daRa2eeseya_ID");
            AddForeignKey("dbo.KharegTmarkoz", "We7daRa2eeseya_ID", "dbo.We7daRa2eeseya", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KharegTmarkoz", "We7daRa2eeseya_ID", "dbo.We7daRa2eeseya");
            DropIndex("dbo.KharegTmarkoz", new[] { "We7daRa2eeseya_ID" });
            DropColumn("dbo.KharegTmarkoz", "We7daRa2eeseya_ID");
        }
    }
}
