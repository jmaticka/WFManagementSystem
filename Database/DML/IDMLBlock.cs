using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DML
{
    interface IDMLBlock
    {
        List<Block> GetAllByWorkflow(Workflow workflow);
        Block Insert(Block block);
        Block Update(Block block);
        Block Delete(Block block);

    }
}
