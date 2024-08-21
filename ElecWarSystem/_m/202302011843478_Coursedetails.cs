namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fer2adetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fer2a",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Fer2aDetailsID = c.Long(nullable: false),
                        TmamID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Fer2aDetails", t => t.Fer2aDetailsID, cascadeDelete: true)
                .ForeignKey("dbo.Tmam", t => t.TmamID, cascadeDelete: true)
                .Index(t => t.Fer2aDetailsID)
                .Index(t => t.TmamID);
            
            CreateTable(
                "dbo.Fer2aDetails",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Fer2aName = c.String(),
                        Fer2aPlace = c.String(),
                        CommandItemID = c.Long(nullable: false),
                        FardID = c.Long(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CommandItems", t => t.CommandItemID, cascadeDelete: true)
                .ForeignKey("dbo.FardDetails", t => t.FardID, cascadeDelete: true)
                .Index(t => t.CommandItemID)
                .Index(t => t.FardID);
            
            AddColumn("dbo.TmamDetails", "Fer2a", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fer2a", "TmamID", "dbo.Tmam");
            DropForeignKey("dbo.Fer2a", "Fer2aDetailsID", "dbo.Fer2aDetails");
            DropForeignKey("dbo.Fer2aDetails", "FardID", "dbo.FardDetails");
            DropForeignKey("dbo.Fer2aDetails", "CommandItemID", "dbo.CommandItems");
            DropIndex("dbo.Fer2aDetails", new[] { "FardID" });
            DropIndex("dbo.Fer2aDetails", new[] { "CommandItemID" });
            DropIndex("dbo.Fer2a", new[] { "TmamID" });
            DropIndex("dbo.Fer2a", new[] { "Fer2aDetailsID" });
            DropColumn("dbo.TmamDetails", "Fer2a");
            DropTable("dbo.Fer2aDetails");
            DropTable("dbo.Fer2a");
        }
    }
}
