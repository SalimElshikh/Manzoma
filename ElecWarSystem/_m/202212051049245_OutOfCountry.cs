namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KharegBelad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KharegBelad",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        KharegBeladDetailID = c.Long(nullable: false),
                        TmamID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KharegBeladDetails", t => t.KharegBeladDetailID, cascadeDelete: true)
                .ForeignKey("dbo.Tmam", t => t.TmamID, cascadeDelete: true)
                .Index(t => t.KharegBeladDetailID)
                .Index(t => t.TmamID);
            
            CreateTable(
                "dbo.KharegBeladDetails",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Balad = c.String(),
                        Sabab = c.String(),
                        FardID = c.Long(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FardDetails", t => t.FardID, cascadeDelete: true)
                .Index(t => t.FardID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KharegBelad", "TmamID", "dbo.Tmam");
            DropForeignKey("dbo.KharegBelad", "KharegBeladDetailID", "dbo.KharegBeladDetails");
            DropForeignKey("dbo.KharegBeladDetails", "FardID", "dbo.FardDetails");
            DropIndex("dbo.KharegBeladDetails", new[] { "FardID" });
            DropIndex("dbo.KharegBelad", new[] { "TmamID" });
            DropIndex("dbo.KharegBelad", new[] { "KharegBeladDetailID" });
            DropTable("dbo.KharegBeladDetails");
            DropTable("dbo.KharegBelad");
        }
    }
}
