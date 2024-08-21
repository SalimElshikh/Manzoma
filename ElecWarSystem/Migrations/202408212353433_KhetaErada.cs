namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KhetaErada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KhetaEradas",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Qa2ed = c.String(),
                        RotbaQa2ed = c.String(),
                        DobatNum = c.Int(nullable: false),
                        DargatNum = c.Int(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        We7daRa2eeseya_ID = c.Int(),
                        Asl7aName_ID = c.Int(nullable: false),
                        MarkbatName_ID = c.Int(nullable: false),
                        Mo3edatName_ID = c.Int(nullable: false),
                        Za5iraName_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Asl7aName", t => t.Asl7aName_ID, cascadeDelete: true)
                .ForeignKey("dbo.MarkbatNames", t => t.MarkbatName_ID, cascadeDelete: true)
                .ForeignKey("dbo.Mo3edatName", t => t.Mo3edatName_ID, cascadeDelete: true)
                .ForeignKey("dbo.We7daRa2eeseya", t => t.We7daRa2eeseya_ID)
                .ForeignKey("dbo.Za5iraName", t => t.Za5iraName_ID, cascadeDelete: true)
                .Index(t => t.We7daRa2eeseya_ID)
                .Index(t => t.Asl7aName_ID)
                .Index(t => t.MarkbatName_ID)
                .Index(t => t.Mo3edatName_ID)
                .Index(t => t.Za5iraName_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KhetaEradas", "Za5iraName_ID", "dbo.Za5iraName");
            DropForeignKey("dbo.KhetaEradas", "We7daRa2eeseya_ID", "dbo.We7daRa2eeseya");
            DropForeignKey("dbo.KhetaEradas", "Mo3edatName_ID", "dbo.Mo3edatName");
            DropForeignKey("dbo.KhetaEradas", "MarkbatName_ID", "dbo.MarkbatNames");
            DropForeignKey("dbo.KhetaEradas", "Asl7aName_ID", "dbo.Asl7aName");
            DropIndex("dbo.KhetaEradas", new[] { "Za5iraName_ID" });
            DropIndex("dbo.KhetaEradas", new[] { "Mo3edatName_ID" });
            DropIndex("dbo.KhetaEradas", new[] { "MarkbatName_ID" });
            DropIndex("dbo.KhetaEradas", new[] { "Asl7aName_ID" });
            DropIndex("dbo.KhetaEradas", new[] { "We7daRa2eeseya_ID" });
            DropTable("dbo.KhetaEradas");
        }
    }
}
