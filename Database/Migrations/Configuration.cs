namespace WFMDatabase.Migrations
{
    using Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WFMDatabase.DBContextWFManagementSystem>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WFMDatabase.DBContextWFManagementSystem context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Garant"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Garant" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "U�ivatel"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "U�ivatel" };

                manager.Create(role);
            }

            if (!(context.Users.Any(u => u.UserName == "admin@test.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "admin@test.com" };
                userManager.Create(userToInsert, "123456");
                userManager.AddToRole(userToInsert.Id, "Admin");
            }


            if (!(context.Users.Any(u => u.UserName == "garant@test.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "garant@test.com" };
                userManager.Create(userToInsert, "123456");
                userManager.AddToRole(userToInsert.Id, "Garant");
            }

            if (!(context.Users.Any(u => u.UserName == "uzivatel@test.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "uzivatel@test.com" };
                userManager.Create(userToInsert, "123456");
                userManager.AddToRole(userToInsert.Id, "U�ivatel");
            }


            if (!(context.BlockTypes.Any(bt => bt.Name == "start")))
            {
                var blockTypeManager = new DML.DMLBlockType();
                var blockType = new Entities.BlockType
                {
                    Name = "start",
                    Description = "Za��tek toku workflow"
                };
                blockTypeManager.Insert(blockType);

            }

            if (!(context.BlockTypes.Any(bt => bt.Name == "aktivita")))
            {
                var blockTypeManager = new DML.DMLBlockType();
                var blockType = new Entities.BlockType
                {
                    Name = "aktivita",
                    Description = "Aktivita ze strany u�ivatele"
                };
                blockTypeManager.Insert(blockType);

            }

            if (!(context.BlockTypes.Any(bt => bt.Name == "soubor")))
            {
                var blockTypeManager = new DML.DMLBlockType();
                var blockType = new Entities.BlockType
                {
                    Name = "soubor",
                    Description = "Nahr�n� souboru do syst�mu"
                };
                blockTypeManager.Insert(blockType);

            }

            if (!(context.BlockTypes.Any(bt => bt.Name == "rozd�len�")))
            {
                var blockTypeManager = new DML.DMLBlockType();
                var blockType = new Entities.BlockType
                {
                    Name = "rozd�len�",
                    Description = "Rozd�len� workflow do v�ce vl�ken"
                };
                blockTypeManager.Insert(blockType);

            }

            if (!(context.BlockTypes.Any(bt => bt.Name == "podm�nka")))
            {
                var blockTypeManager = new DML.DMLBlockType();
                var blockType = new Entities.BlockType
                {
                    Name = "podm�nka",
                    Description = "Sm�r dal��ho procesu"
                };
                blockTypeManager.Insert(blockType);
            }


            if (!(context.BlockTypes.Any(bt => bt.Name == "slou�en�")))
            {
                var blockTypeManager = new DML.DMLBlockType();
                var blockType = new Entities.BlockType
                {
                    Name = "slou�en�",
                    Description = "Slou�en� workflow z v�ce vl�ken"
                };
                blockTypeManager.Insert(blockType);

            }


            if (!(context.BlockTypes.Any(bt => bt.Name == "konec")))
            {
                var blockTypeManager = new DML.DMLBlockType();
                var blockType = new Entities.BlockType
                {
                    Name = "konec",
                    Description = "Konec toku workflow"
                };
                blockTypeManager.Insert(blockType);

            }



        }
    }
}
