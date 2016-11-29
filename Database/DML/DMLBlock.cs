using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public class DMLBlock : IDMLBlock
    {
        public Block Delete(Block block)
        {
            throw new NotImplementedException();
        }

        public List<Block> GetAllByWorkflow(int workflow)
        {
            
            throw new NotImplementedException();
        }

        public Block Insert(Block block)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var res = dbContext.Blocks.Add(block);
                    dbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem pridat block: {0}", e);
            }
        }

        public Block Update(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
