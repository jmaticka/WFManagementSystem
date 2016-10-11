using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Database.DML
{
    class DMLWorkflow : IDMLWorkflow
    {
        private DBContextWFManagementSystem _dbContext;

        public DMLWorkflow()
        {
            _dbContext = DBContextWFManagementSystem.Instance;
        }

        public List<Workflow> GetAllByUser(IdentityUser user)
        {
            var res = _dbContext.Workflows
                .Where(x => x.UserCreated == user)
                .ToList();
            return res;
        }

        public Workflow Insert(Workflow workflow)
        {
            try
            {
                var res = _dbContext.Workflows.Add(workflow);
                _dbContext.SaveChanges();
                return res;
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
                var res = _dbContext.Workflows.Add(workflow);
                _dbContext.SaveChanges();
                return res;
            }
            catch (Exception e)
            {
                throw new Exception("Problem upravit workflow: {0}", e);
            }
        }
        public Workflow Delete(Workflow workflow)
        {
            try { 
                var res = _dbContext.Workflows.Remove(workflow);
                _dbContext.SaveChanges();
                return res;
            }
            catch(Exception e)
            {
                throw new Exception("Problem odebrat workflow: {0}", e);
            }
        }
    }
}
