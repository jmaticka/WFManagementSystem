using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public class DMLBlockType : IDMLBlockType
    {
        public BlockType Delete(BlockType blockType)
        {
            throw new NotImplementedException();
        }

        public List<BlockType> GetAll()
        {
            using (var dbContext = new DBContextWFManagementSystem())
            {
                var res = dbContext.BlockTypes
                        .ToList();
                return res;
            }
        }

        public BlockType Insert(BlockType blockType)
        {
            throw new NotImplementedException();
        }

        public BlockType Update(BlockType blockType)
        {
            throw new NotImplementedException();
        }
    }
}
