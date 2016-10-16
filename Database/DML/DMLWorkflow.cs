using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using WFMDatabase;
using WFMDatabase.DML;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    class DMLWorkflow : IDMLWorkflow
    {
        
        public List<Workflow> GetAllByUser(IdentityUser user)
        {
            using (var dbContext = new DBContextWFManagementSystem())
            {
                var res = dbContext.Workflows
                        .Where(x => x.UserCreated == user)
                        .ToList();
                return res; 
            }
        }

        public Workflow Insert(Workflow workflow)
        {

            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var res = dbContext.Workflows.Add(workflow);
                    dbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem pridat workflow: {0}", e);
            }

        }
        public Workflow Update(Workflow workflow)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var res = dbContext.Workflows.Add(workflow);
                    dbContext.SaveChanges();
                    return res; 
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem upravit workflow: {0}", e);
            }
        }
        public Workflow Delete(Workflow workflow)
        {
            try {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var res = dbContext.Workflows.Remove(workflow);
                    dbContext.SaveChanges();
                    return res;
                }
            }
            catch(Exception e)
            {
                throw new Exception("Problem odebrat workflow: {0}", e);
            }
        }
    }
}
