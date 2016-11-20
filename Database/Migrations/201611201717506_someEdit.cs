namespace WFMDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someEdit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Blocks", "Value");
            DropColumn("dbo.Workflows", "NumberOfBlocks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workflows", "NumberOfBlocks", c => c.Int(nullable: false));
            AddColumn("dbo.Blocks", "Value", c => c.String());
        }
    }
}
