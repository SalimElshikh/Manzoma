namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dump : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fer2aDetails", "CommandItemID", "dbo.CommandItems");
            DropForeignKey("dbo.Fer2aDetails", "FardID", "dbo.FardDetails");
            DropForeignKey("dbo.Fer2a", "Fer2aDetailsID", "dbo.Fer2aDetails");
            DropForeignKey("dbo.Fer2a", "TmamID", "dbo.Tmam");
            DropIndex("dbo.Fer2aDetails", new[] { "CommandItemID" });
            DropIndex("dbo.Fer2aDetails", new[] { "FardID" });
            DropIndex("dbo.Fer2a", new[] { "Fer2aDetailsID" });
            DropIndex("dbo.Fer2a", new[] { "TmamID" });
            RenameColumn(table: "dbo.Agaza", name: "AgazaDetailsID", newName: "AgazaDetailID");
            RenameIndex(table: "dbo.Agaza", name: "IX_AgazaDetailsID", newName: "IX_AgazaDetailID");
            DropColumn("dbo.TmamDetails", "Fer2a");
            DropTable("dbo.Fer2aDetails");
            DropTable("dbo.Fer2a");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Fer2a",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Fer2aDetailsID = c.Long(nullable: false),
                        TmamID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.TmamDetails", "Fer2a", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Agaza", name: "IX_AgazaDetailID", newName: "IX_AgazaDetailsID");
            RenameColumn(table: "dbo.Agaza", name: "AgazaDetailID", newName: "AgazaDetailsID");
            CreateIndex("dbo.Fer2a", "TmamID");
            CreateIndex("dbo.Fer2a", "Fer2aDetailsID");
            CreateIndex("dbo.Fer2aDetails", "FardID");
            CreateIndex("dbo.Fer2aDetails", "CommandItemID");
            AddForeignKey("dbo.Fer2a", "TmamID", "dbo.Tmam", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Fer2a", "Fer2aDetailsID", "dbo.Fer2aDetails", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Fer2aDetails", "FardID", "dbo.FardDetails", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Fer2aDetails", "CommandItemID", "dbo.CommandItems", "ID", cascadeDelete: true);
        }
    }
}
