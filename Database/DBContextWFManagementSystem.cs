using System.Data.Entity;
using WFMDatabase.Entities;

namespace WFMDatabase
{
    public class DBContextWFManagementSystem : DbContext
    {
        
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Field> BlockTypes { get; set; }


    }
}
