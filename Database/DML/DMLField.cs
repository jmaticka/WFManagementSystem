using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WFMDatabase.Entities;

namespace WFMDatabase.DML
{
    public class DMLField : IDMLField
    {
        public Field GetFiled(int fieldId)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var result = dbContext.Fields.Where(x => x.ID == fieldId)
                        .Include(x => x.Block)
                        .Include(x => x.Block.BlockType)
                        .Include(x => x.Block.NextBlocks)
                        .Include(x => x.WorkflowInstance)
                        .FirstOrDefault();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem ziskat field: {0}", e);
            }
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
                throw new Exception("Problem ziskat field na zaklade instance: {0}", e);
            }
        }

        public List<Field> GetAllByWorker(string workerId)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var res = dbContext.Fields
                        .Where(x => x.Worker.Id == workerId)
                        .Where(x => x.IsActive)
                        .Include(x => x.Block)
                        .Include(x => x.Block.BlockType)
                        .Include(x => x.Worker)
                        .ToList();
                    return res;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem ziskat field na zaklade uzivatele: {0}", e);
            }
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

                    var user = field.Worker != null
                        ? dbContext.Users.FirstOrDefault(x => x.UserName == field.Worker.UserName)
                        : null;
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
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var toModify = dbContext.Fields.FirstOrDefault(x => x.ID == field.ID);
                    if (toModify != null)
                    {
                        toModify.DateTimeEnded = field.DateTimeEnded;
                        toModify.IsActive = field.IsActive;
                        toModify.Output = field.Output;
                    }
                    dbContext.SaveChanges();
                    return toModify;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Problem update field: {0}", exception);
            }
        }

        public Field Delete(Field field)
        {
            throw new NotImplementedException();
        }

        public Field GetFieldSuccessorByBlockId(int blockId,int workflowInstanceId)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var result = dbContext.Fields.Where(x => x.Block.ID == blockId && x.WorkflowInstance.ID == workflowInstanceId)
                        .Include(x => x.Block)
                        .Include(x => x.Block.BlockType)
                        .Include(x => x.Block.NextBlocks)
                        .Include(x => x.WorkflowInstance)
                        .FirstOrDefault();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem ziskat field: {0}", e);
            }
        }
    }
}
