namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manzomat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manzomats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        ButtonText = c.String(),
                        ButtonLink = c.String(),
                        ButtonClass = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Manzomats");
        }
    }
}
