namespace ElecWarSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Elsala7eyaEl8aneya", "NumMo2eda", c => c.Int(nullable: false));
            AddColumn("dbo.Elsala7eyaEl8aneya", "El2gra", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Elsala7eyaEl8aneya", "El2gra");
            DropColumn("dbo.Elsala7eyaEl8aneya", "NumMo2eda");
        }
    }
}
