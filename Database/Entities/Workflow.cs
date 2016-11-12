using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFMDatabase.Entities
{
    public class Workflow : EntityBase
    {
        public string Name { get; set; }
        public virtual List<Block> Blocks { get; set; }
        public int NumberOfBlocks { get; set; }
        public bool IsActual { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public ApplicationUser UserCreated { get; set; }
    }
}
