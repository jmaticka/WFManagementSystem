using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Block
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public BlockType BlockType { get; set; }

    }
}
