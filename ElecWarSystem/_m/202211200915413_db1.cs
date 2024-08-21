namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LeaderTmams", "FardID", "dbo.FardDetails");
            DropForeignKey("dbo.LeaderTmams", "TmamID", "dbo.Tmam");
            DropIndex("dbo.LeaderTmams", new[] { "TmamID" });
            DropIndex("dbo.LeaderTmams", new[] { "FardID" });
            DropTable("dbo.LeaderTmams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LeaderTmams",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TmamID = c.Long(nullable: false),
                        TmamType = c.String(),
                        FardID = c.Long(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.LeaderTmams", "FardID");
            CreateIndex("dbo.LeaderTmams", "TmamID");
            AddForeignKey("dbo.LeaderTmams", "TmamID", "dbo.Tmam", "ID", cascadeDelete: true);
            AddForeignKey("dbo.LeaderTmams", "FardID", "dbo.FardDetails", "ID", cascadeDelete: true);
        }
    }
}
