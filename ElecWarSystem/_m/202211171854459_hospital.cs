namespace ElecWarSystem.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Mostashfa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MostashfaDetails",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    Mostashfa = c.String(),
                    Hala = c.String(),
                    Tawseya = c.String(),
                    FardID = c.Long(nullable: false),
                    DateFrom = c.DateTime(nullable: false),
                    DateTo = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FardDetails", t => t.FardID, cascadeDelete: true)
                .Index(t => t.FardID);

            CreateTable(
                "dbo.Mostashfa",
                c => new
                {
                    ID = c.Long(nullable: false, identity: true),
                    MostashfaDetailID = c.Long(nullable: false),
                    TmamID = c.Long(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MostashfaDetails", t => t.MostashfaDetailID, cascadeDelete: true)
                .ForeignKey("dbo.Tmam", t => t.TmamID, cascadeDelete: true)
                .Index(t => t.MostashfaDetailID)
                .Index(t => t.TmamID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Mostashfa", "TmamID", "dbo.Tmam");
            DropForeignKey("dbo.Mostashfa", "MostashfaDetailID", "dbo.SegnDetails");
            DropForeignKey("dbo.MostashfaDetails", "FardID", "dbo.FardDetails");
            DropIndex("dbo.Mostashfa", new[] { "TmamID" });
            DropIndex("dbo.Mostashfa", new[] { "MostashfaDetailID" });
            DropIndex("dbo.MostashfaDetails", new[] { "FardID" });
            DropTable("dbo.Mostashfa");
            DropTable("dbo.MostashfaDetails");
        }
    }
}
