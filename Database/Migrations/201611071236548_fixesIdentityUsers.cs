namespace WFMDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixesIdentityUsers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "IdentityUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.IdentityUsers", newName: "AspNetUsers");
        }
    }
}
