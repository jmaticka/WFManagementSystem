using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public interface IDMLWorkflowInstance
    {
        List<WorkflowInstance> GetAllByWorkflow(Workflow workflow);
        WorkflowInstance Insert(WorkflowInstance workflowInstance);
        WorkflowInstance Update(WorkflowInstance workflowInstance);
        WorkflowInstance Delete(WorkflowInstance workflowInstance);
        List<WorkflowInstance> GetAll();
        List<WorkflowInstance> GetAllByUser(string userId);
    }
}
