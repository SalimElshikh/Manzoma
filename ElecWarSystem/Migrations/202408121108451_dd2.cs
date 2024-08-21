namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnaserMonawba", "Qa2edMonawba", c => c.String());
            AddColumn("dbo.AnaserMonawba", "RotbaQa2edMonawba", c => c.String());
            AddColumn("dbo.AnaserMonawba", "DobatNum", c => c.Int(nullable: false));
            AddColumn("dbo.AnaserMonawba", "DargatNum", c => c.Int(nullable: false));
            AddColumn("dbo.AnaserMonawba", "We7daRa2eeseya_ID", c => c.Int());
            AddColumn("dbo.AnaserMonawba", "Asl7aName_ID", c => c.Int(nullable: false));
            AddColumn("dbo.AnaserMonawba", "MarkbatName_ID", c => c.Int(nullable: false));
            AddColumn("dbo.AnaserMonawba", "Mo3edatName_ID", c => c.Int(nullable: false));
            AddColumn("dbo.AnaserMonawba", "Za5iraName_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.AnaserMonawba", "We7daRa2eeseya_ID");
            CreateIndex("dbo.AnaserMonawba", "Asl7aName_ID");
            CreateIndex("dbo.AnaserMonawba", "MarkbatName_ID");
            CreateIndex("dbo.AnaserMonawba", "Mo3edatName_ID");
            CreateIndex("dbo.AnaserMonawba", "Za5iraName_ID");
            AddForeignKey("dbo.AnaserMonawba", "Asl7aName_ID", "dbo.Asl7aName", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AnaserMonawba", "MarkbatName_ID", "dbo.MarkbatNames", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AnaserMonawba", "Mo3edatName_ID", "dbo.Mo3edatName", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AnaserMonawba", "Za5iraName_ID", "dbo.Za5iraName", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AnaserMonawba", "We7daRa2eeseya_ID", "dbo.We7daRa2eeseya", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnaserMonawba", "We7daRa2eeseya_ID", "dbo.We7daRa2eeseya");
            DropForeignKey("dbo.AnaserMonawba", "Za5iraName_ID", "dbo.Za5iraName");
            DropForeignKey("dbo.AnaserMonawba", "Mo3edatName_ID", "dbo.Mo3edatName");
            DropForeignKey("dbo.AnaserMonawba", "MarkbatName_ID", "dbo.MarkbatNames");
            DropForeignKey("dbo.AnaserMonawba", "Asl7aName_ID", "dbo.Asl7aName");
            DropIndex("dbo.AnaserMonawba", new[] { "Za5iraName_ID" });
            DropIndex("dbo.AnaserMonawba", new[] { "Mo3edatName_ID" });
            DropIndex("dbo.AnaserMonawba", new[] { "MarkbatName_ID" });
            DropIndex("dbo.AnaserMonawba", new[] { "Asl7aName_ID" });
            DropIndex("dbo.AnaserMonawba", new[] { "We7daRa2eeseya_ID" });
            DropColumn("dbo.AnaserMonawba", "Za5iraName_ID");
            DropColumn("dbo.AnaserMonawba", "Mo3edatName_ID");
            DropColumn("dbo.AnaserMonawba", "MarkbatName_ID");
            DropColumn("dbo.AnaserMonawba", "Asl7aName_ID");
            DropColumn("dbo.AnaserMonawba", "We7daRa2eeseya_ID");
            DropColumn("dbo.AnaserMonawba", "DargatNum");
            DropColumn("dbo.AnaserMonawba", "DobatNum");
            DropColumn("dbo.AnaserMonawba", "RotbaQa2edMonawba");
            DropColumn("dbo.AnaserMonawba", "Qa2edMonawba");
        }
    }
}
