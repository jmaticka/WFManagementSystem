using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Block : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public List<Block> NextBlocks { get; set; }
        public virtual BlockType BlockType { get; set; }
        public IdentityUser Worker { get; set; }

    }
}
