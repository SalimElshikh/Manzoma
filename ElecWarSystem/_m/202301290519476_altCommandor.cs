namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altCommandor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.We7daRa2eeseya", "TawagodQa2edManoob", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.We7daRa2eeseya", "TawagodQa2edManoob");
        }
    }
}
