
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    interface IDMLWorkflow
    {
        List<Workflow> GetAllByUser(IdentityUser user);
        Workflow Insert(Workflow workflow);
        Workflow Update(Workflow workflow);
        Workflow Delete(Workflow workflow);

    }
}
