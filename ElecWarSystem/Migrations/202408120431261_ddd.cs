namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mot8ayeratEst3dadQetali", "A8radTa7arok_ID", "dbo.A8radTa7arok");
            DropForeignKey("dbo.Mot8ayeratEst3dadQetali", "Al7ala_ID", "dbo.Al7ala");
            DropForeignKey("dbo.Mot8ayeratEst3dadQetali", "Ge7aTasdek_ID", "dbo.Ge7aTasdek");
            DropIndex("dbo.Mot8ayeratEst3dadQetali", new[] { "Al7ala_ID" });
            DropIndex("dbo.Mot8ayeratEst3dadQetali", new[] { "Ge7aTasdek_ID" });
            DropIndex("dbo.Mot8ayeratEst3dadQetali", new[] { "A8radTa7arok_ID" });
            CreateTable(
                "dbo.Erada",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Al7ala", "Name");
            DropTable("dbo.Ge7aTasdek");
            DropTable("dbo.Mot8ayeratEst3dadQetali");
            DropTable("dbo.Mowasalat");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Mowasalat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EstrategyQa2ed = c.Boolean(nullable: false),
                        EstrategyMarkazAmaliat = c.Boolean(nullable: false),
                        EstrategyTa7wela = c.Boolean(nullable: false),
                        SentralQa2ed = c.Boolean(nullable: false),
                        SentralMarkazAmaliat = c.Boolean(nullable: false),
                        MowaslatLaselkya = c.Boolean(nullable: false),
                        TransfarData = c.Boolean(nullable: false),
                        HotNumber = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Mot8ayeratEst3dadQetali",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Item = c.String(nullable: false),
                        Al7ala_ID = c.Int(),
                        Ge7aTasdek_ID = c.Int(),
                        A8radTa7arok_ID = c.Int(),
                        DateTo = c.DateTime(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ge7aTasdek",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Al7ala", "Name", c => c.String());
            DropTable("dbo.Erada");
            CreateIndex("dbo.Mot8ayeratEst3dadQetali", "A8radTa7arok_ID");
            CreateIndex("dbo.Mot8ayeratEst3dadQetali", "Ge7aTasdek_ID");
            CreateIndex("dbo.Mot8ayeratEst3dadQetali", "Al7ala_ID");
            AddForeignKey("dbo.Mot8ayeratEst3dadQetali", "Ge7aTasdek_ID", "dbo.Ge7aTasdek", "ID");
            AddForeignKey("dbo.Mot8ayeratEst3dadQetali", "Al7ala_ID", "dbo.Al7ala", "ID");
            AddForeignKey("dbo.Mot8ayeratEst3dadQetali", "A8radTa7arok_ID", "dbo.A8radTa7arok", "ID");
        }
    }
}
