using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DML
{
    interface IDMLBlockType
    {
        BlockType Insert(BlockType blockType);
        BlockType Update(BlockType blockType);
        BlockType Delete(BlockType blockType);
    }
}
