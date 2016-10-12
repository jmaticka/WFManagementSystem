using Database.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DML
{
    interface IDMLField
    {
        List<Field> GetAllByBlock(Block block);
        List<Field> GetAllByInstance(int workflowInstanceId);
        List<Field> GetAllByWorker(IdentityUser worker);
        Field Insert(Field field);
        Field Update(Field field);
        Field Delete(Field field);

    }
}
