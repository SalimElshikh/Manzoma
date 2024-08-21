namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mo3askr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mo3askrDetails",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Twagod = c.String(),
                        Sabab = c.String(),
                        FardID = c.Long(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FardDetails", t => t.FardID, cascadeDelete: true)
                .Index(t => t.FardID);
            
            CreateTable(
                "dbo.Mo3askr",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Mo3askrDetailsID = c.Long(nullable: false),
                        TmamID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Mo3askrDetails", t => t.Mo3askrDetailsID, cascadeDelete: true)
                .ForeignKey("dbo.Tmam", t => t.TmamID, cascadeDelete: true)
                .Index(t => t.Mo3askrDetailsID)
                .Index(t => t.TmamID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mo3askr", "TmamID", "dbo.Tmam");
            DropForeignKey("dbo.Mo3askr", "Mo3askrDetailsID", "dbo.Mo3askrDetails");
            DropForeignKey("dbo.Mo3askrDetails", "FardID", "dbo.FardDetails");
            DropIndex("dbo.Mo3askr", new[] { "TmamID" });
            DropIndex("dbo.Mo3askr", new[] { "Mo3askrDetailsID" });
            DropIndex("dbo.Mo3askrDetails", new[] { "FardID" });
            DropTable("dbo.Mo3askr");
            DropTable("dbo.Mo3askrDetails");
        }
    }
}
