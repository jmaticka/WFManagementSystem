namespace WFMDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalFox : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Value = c.String(),
                        BlockType_ID = c.Int(),
                        Block_ID = c.Int(),
                        Worker_Id = c.String(maxLength: 128),
                        Workflow_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlockTypes", t => t.BlockType_ID)
                .ForeignKey("dbo.Blocks", t => t.Block_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Worker_Id)
                .ForeignKey("dbo.Workflows", t => t.Workflow_ID)
                .Index(t => t.BlockType_ID)
                .Index(t => t.Block_ID)
                .Index(t => t.Worker_Id)
                .Index(t => t.Workflow_ID);
            
            CreateTable(
                "dbo.BlockTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                .ForeignKey("dbo.AspNetUsers", t => t.Worker_Id)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserStarted_Id)
                .ForeignKey("dbo.Workflows", t => t.Workflow_ID)
                .Index(t => t.UserStarted_Id)
                .Index(t => t.Workflow_ID);
            
            CreateTable(
                "dbo.Workflows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumberOfBlocks = c.Int(nullable: false),
                        IsActual = c.Boolean(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        UserCreated_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserCreated_Id)
                .Index(t => t.UserCreated_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Fields", "WorkflowInstance_ID", "dbo.WorkflowInstances");
            DropForeignKey("dbo.WorkflowInstances", "Workflow_ID", "dbo.Workflows");
            DropForeignKey("dbo.Workflows", "UserCreated_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Blocks", "Workflow_ID", "dbo.Workflows");
            DropForeignKey("dbo.WorkflowInstances", "UserStarted_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fields", "Worker_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fields", "Block_ID", "dbo.Blocks");
            DropForeignKey("dbo.Blocks", "Worker_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Blocks", "Block_ID", "dbo.Blocks");
            DropForeignKey("dbo.Blocks", "BlockType_ID", "dbo.BlockTypes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Workflows", new[] { "UserCreated_Id" });
            DropIndex("dbo.WorkflowInstances", new[] { "Workflow_ID" });
            DropIndex("dbo.WorkflowInstances", new[] { "UserStarted_Id" });
            DropIndex("dbo.Fields", new[] { "WorkflowInstance_ID" });
            DropIndex("dbo.Fields", new[] { "Worker_Id" });
            DropIndex("dbo.Fields", new[] { "Block_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Blocks", new[] { "Workflow_ID" });
            DropIndex("dbo.Blocks", new[] { "Worker_Id" });
            DropIndex("dbo.Blocks", new[] { "Block_ID" });
            DropIndex("dbo.Blocks", new[] { "BlockType_ID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Workflows");
            DropTable("dbo.WorkflowInstances");
            DropTable("dbo.Fields");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BlockTypes");
            DropTable("dbo.Blocks");
        }
    }
}
