using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public class DMLField: IDMLField
    {
        public List<Field> GetAllByBlock(Block block)
        {
            throw new NotImplementedException();
        }

        public List<Field> GetAllByInstance(int workflowInstanceId)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var res = dbContext.Fields
                        .Where(x => x.WorkflowInstance.ID == workflowInstanceId)
                        .Include(x => x.Block)
                        .Include(x => x.Block.BlockType)
                        .Include(x => x.Worker)
                        .ToList();
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem ziskat workflow instanci: {0}", e);
            }
        }

        public List<Field> GetAllByWorker(IdentityUser worker)
        {
            throw new NotImplementedException();
        }

        public List<Field> Insert(List<Field> fields)
        {
            return fields.Select(Insert).ToList();
        }

        public Field Insert(Field field)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    
                    var user =field.Worker != null ? dbContext.Users.FirstOrDefault(x => x.UserName == field.Worker.UserName): null;
                    field.Worker = user;

                    var workflowInstance =
                        dbContext.WorkflowInstances.FirstOrDefault(x => x.ID == field.WorkflowInstance.ID);
                    field.WorkflowInstance = workflowInstance;
                    if (workflowInstance != null) field.DateTimeEnded = workflowInstance.DateTimeEnded;
                    var block = dbContext.Blocks.FirstOrDefault(x => x.ID == field.Block.ID);
                    field.Block = block;

                   var res = dbContext.Fields.Add(field);
                    dbContext.SaveChanges();
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem pridat workflow instanci: {0}", e);
            }
        }

        public Field Update(Field field)
        {
            throw new NotImplementedException();
        }

        public Field Delete(Field field)
        {
            throw new NotImplementedException();
        }
    }
}
