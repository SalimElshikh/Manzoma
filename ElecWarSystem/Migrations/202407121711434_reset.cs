namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reset : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KharegTmarkoz", "ID", "dbo.FardDetails");
            DropForeignKey("dbo.Asl7a", "KharegTmarkozs_ID", "dbo.KharegTmarkoz");
            DropForeignKey("dbo.Markbat", "KharegTmarkozs_ID", "dbo.KharegTmarkoz");
            DropForeignKey("dbo.Mo3edat", "KharegTmarkozs_ID", "dbo.KharegTmarkoz");
            DropForeignKey("dbo.Za5ira", "KharegTmarkozs_ID", "dbo.KharegTmarkoz");
            DropIndex("dbo.KharegTmarkoz", new[] { "ID" });
            DropPrimaryKey("dbo.KharegTmarkoz");
            AlterColumn("dbo.KharegTmarkoz", "ID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.KharegTmarkoz", "ID");
            AddForeignKey("dbo.Asl7a", "KharegTmarkozs_ID", "dbo.KharegTmarkoz", "ID");
            AddForeignKey("dbo.Markbat", "KharegTmarkozs_ID", "dbo.KharegTmarkoz", "ID");
            AddForeignKey("dbo.Mo3edat", "KharegTmarkozs_ID", "dbo.KharegTmarkoz", "ID");
            AddForeignKey("dbo.Za5ira", "KharegTmarkozs_ID", "dbo.KharegTmarkoz", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Za5ira", "KharegTmarkozs_ID", "dbo.KharegTmarkoz");
            DropForeignKey("dbo.Mo3edat", "KharegTmarkozs_ID", "dbo.KharegTmarkoz");
            DropForeignKey("dbo.Markbat", "KharegTmarkozs_ID", "dbo.KharegTmarkoz");
            DropForeignKey("dbo.Asl7a", "KharegTmarkozs_ID", "dbo.KharegTmarkoz");
            DropPrimaryKey("dbo.KharegTmarkoz");
            AlterColumn("dbo.KharegTmarkoz", "ID", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.KharegTmarkoz", "ID");
            CreateIndex("dbo.KharegTmarkoz", "ID");
            AddForeignKey("dbo.Za5ira", "KharegTmarkozs_ID", "dbo.KharegTmarkoz", "ID");
            AddForeignKey("dbo.Mo3edat", "KharegTmarkozs_ID", "dbo.KharegTmarkoz", "ID");
            AddForeignKey("dbo.Markbat", "KharegTmarkozs_ID", "dbo.KharegTmarkoz", "ID");
            AddForeignKey("dbo.Asl7a", "KharegTmarkozs_ID", "dbo.KharegTmarkoz", "ID");
            AddForeignKey("dbo.KharegTmarkoz", "ID", "dbo.FardDetails", "ID");
        }
    }
}
