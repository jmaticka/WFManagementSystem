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

        public BlockType GetById(int Id)
        {
            var res = GetAll().FirstOrDefault(x => x.ID == Id);
            return res;
        }

        public BlockType Insert(BlockType blockType)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var res = dbContext.BlockTypes.Add(blockType);
                    dbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem pridat typ workflow: {0}", e);
            }
        }

        public BlockType Update(BlockType blockType)
        {
            throw new NotImplementedException();
        }
    }
}
