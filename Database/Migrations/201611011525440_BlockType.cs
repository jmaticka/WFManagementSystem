namespace WFMDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlockType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fields", "Block_ID", "dbo.Blocks");
            DropForeignKey("dbo.Fields", "Worker_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.WorkflowInstances", "UserStarted_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.WorkflowInstances", "Workflow_ID", "dbo.Workflows");
            DropForeignKey("dbo.Fields", "WorkflowInstance_ID", "dbo.WorkflowInstances");
            DropIndex("dbo.Fields", new[] { "Block_ID" });
            DropIndex("dbo.Fields", new[] { "Worker_Id" });
            DropIndex("dbo.Fields", new[] { "WorkflowInstance_ID" });
            DropIndex("dbo.WorkflowInstances", new[] { "UserStarted_Id" });
            DropIndex("dbo.WorkflowInstances", new[] { "Workflow_ID" });
            DropTable("dbo.Fields");
            DropTable("dbo.WorkflowInstances");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.WorkflowInstances", "Workflow_ID");
            CreateIndex("dbo.WorkflowInstances", "UserStarted_Id");
            CreateIndex("dbo.Fields", "WorkflowInstance_ID");
            CreateIndex("dbo.Fields", "Worker_Id");
            CreateIndex("dbo.Fields", "Block_ID");
            AddForeignKey("dbo.Fields", "WorkflowInstance_ID", "dbo.WorkflowInstances", "ID");
            AddForeignKey("dbo.WorkflowInstances", "Workflow_ID", "dbo.Workflows", "ID");
            AddForeignKey("dbo.WorkflowInstances", "UserStarted_Id", "dbo.IdentityUsers", "Id");
            AddForeignKey("dbo.Fields", "Worker_Id", "dbo.IdentityUsers", "Id");
            AddForeignKey("dbo.Fields", "Block_ID", "dbo.Blocks", "ID");
        }
    }
}
