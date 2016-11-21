using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFMDatabase.Entities
{
    public class Block : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Block> NextBlocks { get; set; }
        public string Position { get; set; }
        public virtual BlockType BlockType { get; set; }
        public ApplicationUser Worker { get; set; }

    }
}
