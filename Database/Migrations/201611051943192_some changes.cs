namespace WFMDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        DateTimeEnded = c.DateTime(nullable: false),
                        Block_ID = c.Int(),
                        Worker_Id = c.String(maxLength: 128),
                        WorkflowInstance_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Blocks", t => t.Block_ID)
                .ForeignKey("dbo.IdentityUsers", t => t.Worker_Id)
                .ForeignKey("dbo.WorkflowInstances", t => t.WorkflowInstance_ID)
                .Index(t => t.Block_ID)
                .Index(t => t.Worker_Id)
                .Index(t => t.WorkflowInstance_ID);
            
            CreateTable(
                "dbo.WorkflowInstances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTimeStarted = c.DateTime(nullable: false),
                        DateTimeEnded = c.DateTime(nullable: false),
                        UserStarted_Id = c.String(maxLength: 128),
                        Workflow_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.IdentityUsers", t => t.UserStarted_Id)
                .ForeignKey("dbo.Workflows", t => t.Workflow_ID)
                .Index(t => t.UserStarted_Id)
                .Index(t => t.Workflow_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fields", "WorkflowInstance_ID", "dbo.WorkflowInstances");
            DropForeignKey("dbo.WorkflowInstances", "Workflow_ID", "dbo.Workflows");
            DropForeignKey("dbo.WorkflowInstances", "UserStarted_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Fields", "Worker_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Fields", "Block_ID", "dbo.Blocks");
            DropIndex("dbo.WorkflowInstances", new[] { "Workflow_ID" });
            DropIndex("dbo.WorkflowInstances", new[] { "UserStarted_Id" });
            DropIndex("dbo.Fields", new[] { "WorkflowInstance_ID" });
            DropIndex("dbo.Fields", new[] { "Worker_Id" });
            DropIndex("dbo.Fields", new[] { "Block_ID" });
            DropTable("dbo.WorkflowInstances");
            DropTable("dbo.Fields");
        }
    }
}
