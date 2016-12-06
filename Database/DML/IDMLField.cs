using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public interface IDMLField
    {
        List<Field> GetAllByBlock(Block block);
        List<Field> GetAllByInstance(int workflowInstanceId);
        List<Field> GetAllByWorker(IdentityUser worker);
        List<Field> Insert(List<Field> field);
        Field Insert(Field field);
        Field Update(Field field);
        Field Delete(Field field);

    }
}
