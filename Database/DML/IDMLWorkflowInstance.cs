using Database.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DML
{
    interface IDMLWorkflowInstance
    {
        List<WorkflowInstance> GetAllByUser(IdentityUser user);
        List<WorkflowInstance> GetAllByWorkflow(Workflow workflow);
        WorkflowInstance Insert(WorkflowInstance workflowInstance);
        WorkflowInstance Update(WorkflowInstance workflowInstance);
        WorkflowInstance Delete(WorkflowInstance workflowInstance);
    }
}
