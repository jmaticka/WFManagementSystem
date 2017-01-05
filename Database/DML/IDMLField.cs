using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public interface IDMLField
    {
        Field GetFiled(int fieldId);
        List<Field> GetAllByInstance(int workflowInstanceId);
        List<Field> GetAllByWorker(string workerId);
        List<Field> Insert(List<Field> field);
        Field Insert(Field field);
        Field Update(Field field);
        Field UpdateUser(Field field);
        Field Delete(Field field);
        Field GetFieldSuccessorByBlockId(int blockId, int workflowInstanceId);

    }
}
