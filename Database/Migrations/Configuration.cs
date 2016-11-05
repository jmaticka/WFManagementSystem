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
            ContextKey = "WFMDatabase.DBContextWFManagementSystem";
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

            if (!context.Roles.Any(r => r.Name == "Uživatel"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Uživatel" };

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
                userManager.AddToRole(userToInsert.Id, "Uživatel");
            }


        }
    }
}
