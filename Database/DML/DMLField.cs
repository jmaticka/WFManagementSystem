using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    class DMLField: IDMLField
    {
        public List<Field> GetAllByBlock(Block block)
        {
            throw new NotImplementedException();
        }

        public List<Field> GetAllByInstance(int workflowInstanceId)
        {
            throw new NotImplementedException();
        }

        public List<Field> GetAllByWorker(IdentityUser worker)
        {
            throw new NotImplementedException();
        }

        public Field Insert(Field field)
        {
            throw new NotImplementedException();
        }

        public Field Update(Field field)
        {
            throw new NotImplementedException();
        }

        public Field Delete(Field field)
        {
            throw new NotImplementedException();
        }
    }
}
