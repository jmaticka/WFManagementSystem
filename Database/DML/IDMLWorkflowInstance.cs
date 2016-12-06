using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public interface IDMLWorkflowInstance
    {
        WorkflowInstance GetById(int id);
        List<WorkflowInstance> GetAllByWorkflow(Workflow workflow);
        WorkflowInstance Insert(WorkflowInstance workflowInstance, string userId);
        WorkflowInstance Update(WorkflowInstance workflowInstance);
        WorkflowInstance Delete(WorkflowInstance workflowInstance);
        List<WorkflowInstance> GetAll();
        List<WorkflowInstance> GetAllByUser(string userId);
    }
}
