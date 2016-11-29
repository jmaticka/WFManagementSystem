using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public class DMLWorkflowInstances: IDMLWorkflowInstance
    {
        public List<WorkflowInstance> GetAllByWorkflow(Workflow workflow)
        {
            throw new NotImplementedException();
        }

        public WorkflowInstance Insert(WorkflowInstance workflowInstance)
        {
            throw new NotImplementedException();
        }

        public WorkflowInstance Update(WorkflowInstance workflowInstance)
        {
            throw new NotImplementedException();
        }

        public WorkflowInstance Delete(WorkflowInstance workflowInstance)
        {
            throw new NotImplementedException();
        }

        public List<WorkflowInstance> GetAll()
        {
            using (var db = new DBContextWFManagementSystem())
            {
                return db.WorkflowInstances.Where(x=>x.DateTimeEnded!=null).ToList();
            }
        }

        public List<WorkflowInstance> GetAllByUser(string userId)
        {
            using (var db = new DBContextWFManagementSystem())
            {
                return db.WorkflowInstances.Where(x => x.UserStarted.Id == userId).ToList();
            }
        }
    }
}
