namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Roles", c => c.Byte(nullable: false));
            DropTable("dbo.OfficcerTypes");
            DropTable("dbo.Weapons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Weapons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OfficcerTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Users", "Roles");
        }
    }
}
