namespace WFMDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                .ForeignKey("dbo.IdentityUsers", t => t.Worker_Id)
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
                "dbo.IdentityUsers",
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
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityUsers", t => t.IdentityUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
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
                .ForeignKey("dbo.IdentityUsers", t => t.UserCreated_Id)
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
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Fields", "WorkflowInstance_ID", "dbo.WorkflowInstances");
            DropForeignKey("dbo.WorkflowInstances", "Workflow_ID", "dbo.Workflows");
            DropForeignKey("dbo.Workflows", "UserCreated_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Blocks", "Workflow_ID", "dbo.Workflows");
            DropForeignKey("dbo.WorkflowInstances", "UserStarted_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Fields", "Worker_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Fields", "Block_ID", "dbo.Blocks");
            DropForeignKey("dbo.Blocks", "Worker_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Blocks", "Block_ID", "dbo.Blocks");
            DropForeignKey("dbo.Blocks", "BlockType_ID", "dbo.BlockTypes");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Workflows", new[] { "UserCreated_Id" });
            DropIndex("dbo.WorkflowInstances", new[] { "Workflow_ID" });
            DropIndex("dbo.WorkflowInstances", new[] { "UserStarted_Id" });
            DropIndex("dbo.Fields", new[] { "WorkflowInstance_ID" });
            DropIndex("dbo.Fields", new[] { "Worker_Id" });
            DropIndex("dbo.Fields", new[] { "Block_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUsers", "UserNameIndex");
            DropIndex("dbo.Blocks", new[] { "Workflow_ID" });
            DropIndex("dbo.Blocks", new[] { "Worker_Id" });
            DropIndex("dbo.Blocks", new[] { "Block_ID" });
            DropIndex("dbo.Blocks", new[] { "BlockType_ID" });
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Workflows");
            DropTable("dbo.WorkflowInstances");
            DropTable("dbo.Fields");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.IdentityUsers");
            DropTable("dbo.BlockTypes");
            DropTable("dbo.Blocks");
        }
    }
}
