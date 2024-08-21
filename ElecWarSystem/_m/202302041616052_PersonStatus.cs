namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fard",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TmamID = c.Long(nullable: false),
                        FardID = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FardDetails", t => t.FardID, cascadeDelete: true)
                .ForeignKey("dbo.Tmam", t => t.TmamID, cascadeDelete: true)
                .Index(t => t.TmamID)
                .Index(t => t.FardID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fard", "TmamID", "dbo.Tmam");
            DropForeignKey("dbo.Fard", "FardID", "dbo.FardDetails");
            DropIndex("dbo.Fard", new[] { "FardID" });
            DropIndex("dbo.Fard", new[] { "TmamID" });
            DropTable("dbo.Fard");
        }
    }
}
