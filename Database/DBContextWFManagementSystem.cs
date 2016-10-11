using Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DBContextWFManagementSystem : DbContext
    {
        public static readonly DBContextWFManagementSystem Instance = new DBContextWFManagementSystem();
        private DBContextWFManagementSystem() {}
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Field> BlockTypes { get; set; }


    }
}
