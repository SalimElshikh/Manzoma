namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Elsala7eyaEl8aneya",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameMo2eda = c.String(),
                        MakanEsla7_ID = c.Int(),
                        MostawaEIsla7_ID = c.Int(),
                        TypeMo2eda_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MakanEsla7", t => t.MakanEsla7_ID)
                .ForeignKey("dbo.MostawaEIsla7", t => t.MostawaEIsla7_ID)
                .ForeignKey("dbo.TypeMo2eda", t => t.TypeMo2eda_ID)
                .Index(t => t.MakanEsla7_ID)
                .Index(t => t.MostawaEIsla7_ID)
                .Index(t => t.TypeMo2eda_ID);
            
            CreateTable(
                "dbo.MakanEsla7",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MostawaEIsla7",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TypeMo2eda",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.MostawaIsla7");
            DropTable("dbo.MwaslatW7dat");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MwaslatW7dat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MostawaIsla7",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Elsala7eyaEl8aneya", "TypeMo2eda_ID", "dbo.TypeMo2eda");
            DropForeignKey("dbo.Elsala7eyaEl8aneya", "MostawaEIsla7_ID", "dbo.MostawaEIsla7");
            DropForeignKey("dbo.Elsala7eyaEl8aneya", "MakanEsla7_ID", "dbo.MakanEsla7");
            DropIndex("dbo.Elsala7eyaEl8aneya", new[] { "TypeMo2eda_ID" });
            DropIndex("dbo.Elsala7eyaEl8aneya", new[] { "MostawaEIsla7_ID" });
            DropIndex("dbo.Elsala7eyaEl8aneya", new[] { "MakanEsla7_ID" });
            DropTable("dbo.TypeMo2eda");
            DropTable("dbo.MostawaEIsla7");
            DropTable("dbo.MakanEsla7");
            DropTable("dbo.Elsala7eyaEl8aneya");
        }
    }
}
