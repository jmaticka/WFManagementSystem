﻿using System;
using System.Collections.Generic;
using System.Linq;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public class DMLWorkflow : IDMLWorkflow
    {
        public List<Workflow> GetAll()
        {
            using (var dbContext = new DBContextWFManagementSystem())
            {
                var res = dbContext.Workflows
                        .Where(x => x.IsActual == true)
                        .ToList();
                return res;
            }
        }

        public List<Workflow> GetAllByUser(string userId)
        {
            using (var dbContext = new DBContextWFManagementSystem())
            {
                var res = dbContext.Workflows
                        .Where(x => x.UserCreated.Id == userId)
                        .Where(x => x.IsActual == true)
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
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var res = dbContext.Workflows.Remove(workflow);
                    dbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem odebrat workflow: {0}", e);
            }
        }


    }
}
