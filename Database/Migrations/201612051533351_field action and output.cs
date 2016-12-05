namespace WFMDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fieldactionandoutput : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fields", "Action", c => c.String());
            AddColumn("dbo.Fields", "Output", c => c.String());
            DropColumn("dbo.Fields", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fields", "Value", c => c.String());
            DropColumn("dbo.Fields", "Output");
            DropColumn("dbo.Fields", "Action");
        }
    }
}
