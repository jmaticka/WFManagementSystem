using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Workflow
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Block> Blocks { get; set; }
        public int NumberOfBlocks { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public IdentityUser UserCreated { get; set; }
        public DateTime DateTimeModified { get; set; }
        public IdentityUser UserModified { get; set; }
    }
}
