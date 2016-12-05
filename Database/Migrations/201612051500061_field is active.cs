namespace WFMDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fieldisactive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fields", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fields", "IsActive");
        }
    }
}
