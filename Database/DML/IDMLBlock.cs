
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public interface IDMLBlock
    {
        List<Block> GetAllByWorkflow(int workflow);
        Block Insert(Block block);
        Block Update(Block block);
        Block Delete(Block block);

    }
}
