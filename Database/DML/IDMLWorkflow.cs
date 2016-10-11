using Database.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DML
{
    interface IDMLWorkflow
    {
        List<Workflow> GetAllByUser(IdentityUser user);
        Workflow Insert(Workflow workflow);
        Workflow Update(Workflow workflow);
        Workflow Delete(Workflow workflow);

    }
}
