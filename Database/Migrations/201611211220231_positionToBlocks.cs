namespace WFMDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class positionToBlocks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blocks", "Position", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blocks", "Position");
        }
    }
}
