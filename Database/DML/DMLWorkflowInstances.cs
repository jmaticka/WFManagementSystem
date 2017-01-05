using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public class DMLWorkflowInstances : IDMLWorkflowInstance
    {
        public WorkflowInstance GetById(int id)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    return dbContext.WorkflowInstances
                        .Include(x => x.Workflow)
                        .Include(x => x.UserStarted)
                        .FirstOrDefault(x => x.ID == id)
                    ;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem ziskat workflow instanci: {0}", e);
            }
        }

        public List<WorkflowInstance> GetAllByWorkflow(Workflow workflow)
        {
            throw new NotImplementedException();
        }

        public WorkflowInstance Insert(WorkflowInstance workflowInstance, string userId)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
                    workflowInstance.UserStarted = user;
                    workflowInstance.DateTimeStarted = DateTime.Now;
                    workflowInstance.DateTimeEnded = DateTime.Now;
                    var workflow = dbContext.Workflows.FirstOrDefault(x => x.ID == workflowInstance.Workflow.ID);
                    workflowInstance.Workflow = workflow;
                    //var tracked = dbContext.ChangeTracker.Entries<Workflow>().Any(e => e.Entity.ID == workflowInstance.Workflow.ID);
                    //if (!tracked)
                    //    dbContext.Workflows.Attach(workflowInstance.Workflow);
                    var res = dbContext.WorkflowInstances.Add(workflowInstance);
                    dbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem pridat workflow instanci: {0}", e);
            }

        }

        public WorkflowInstance Update(WorkflowInstance workflowInstance)
        {
            
            using (var db = new DBContextWFManagementSystem())
            {
                var oldWorkFlowInstance = db.WorkflowInstances.FirstOrDefault(x => x.ID == workflowInstance.ID);
                if (oldWorkFlowInstance != null) oldWorkFlowInstance.DateTimeEnded = workflowInstance.DateTimeEnded;
                db.SaveChanges();
                return workflowInstance;
            }
        }

        public WorkflowInstance Delete(WorkflowInstance workflowInstance)
        {
            throw new NotImplementedException();
        }

        public List<WorkflowInstance> GetAll()
        {
            using (var db = new DBContextWFManagementSystem())
            {
                return db.WorkflowInstances.Where(x => x.DateTimeEnded != null)
                    .Include(x => x.Workflow)
                    .Include(x => x.UserStarted).ToList();
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
