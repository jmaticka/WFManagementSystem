
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public interface IDMLWorkflow
    {
        List<Workflow> GetAll();
        List<Workflow> GetAllByUser(string userId);
        Workflow Insert(Workflow workflow, string userId);
        Workflow Update(Workflow newWorkflow);
        Workflow Delete(Workflow workflow);

    }
}
