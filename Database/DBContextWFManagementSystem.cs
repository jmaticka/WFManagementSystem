using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WFMDatabase.Entities;

namespace WFMDatabase
{

    public class DBContextWFManagementSystem : IdentityDbContext<ApplicationUser>
    {

        public DBContextWFManagementSystem() : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
        
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<BlockType> BlockTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        public static DBContextWFManagementSystem Create()
        {
            return new DBContextWFManagementSystem();
        }

    }
}
