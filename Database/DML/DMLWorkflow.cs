using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
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

        public Workflow GetById(int id)
        {
            using (var dbContext = new DBContextWFManagementSystem())
            {
                var res = dbContext.Workflows
                    .Include(x => x.Blocks.Select(y => y.BlockType))
                    .Include(x => x.Blocks.Select(y => y.NextBlocks))
                    .Include(x => x.UserCreated)
                    .Include(x => x.Blocks.Select(y => y.Worker))
                    .FirstOrDefault(x => x.ID == id);


                return res;
            }
        }

        public Workflow Insert(Workflow workflow, string userId)
        {

            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
                    workflow.UserCreated = user;

                    //var tempWorkflow = new Workflow();
                    //tempWorkflow.DateTimeCreated = workflow.DateTimeCreated;
                    //tempWorkflow.IsActual = workflow.IsActual;
                    //tempWorkflow.Name = workflow.Name;
                    //tempWorkflow.UserCreated = workflow.UserCreated;

                    //dbContext.Workflows.Add(tempWorkflow);


                    for (int i = workflow.Blocks.Count - 1; i > -1; i--)
                    {
                        var tracked = dbContext.ChangeTracker.Entries<BlockType>().Any(e => e.Entity.ID == workflow.Blocks[i].BlockType.ID);
                        if (!tracked)
                            dbContext.BlockTypes.Attach(workflow.Blocks[i].BlockType);
                        else
                        {
                            var id = workflow.Blocks[i].BlockType.ID;
                            workflow.Blocks[i].BlockType =
                                    dbContext.BlockTypes.FirstOrDefault(x => x.ID == id);
                        }

                        //foreach (var workflowBlockNextBlock in workflowBlock.NextBlocks)
                        //{
                        //    var tracked2 = dbContext.ChangeTracker.Entries<BlockType>().Any(e => e.Entity.ID == workflowBlockNextBlock.BlockType.ID);
                        //    if (!tracked2)
                        //        dbContext.BlockTypes.Attach(workflowBlockNextBlock.BlockType);
                        //    else
                        //    {
                        //        workflowBlockNextBlock.BlockType =
                        //            dbContext.BlockTypes.FirstOrDefault(x => x.ID == workflowBlockNextBlock.BlockType.ID);
                        //    }
                        //}

                        if (workflow.Blocks[i].NextBlocks.Count > 0)
                        {
                            for (int j = 0; j < workflow.Blocks[i].NextBlocks.Count; j++)
                            {
                                var trackedNextBlock = dbContext.ChangeTracker.Entries<Block>().Any(e => e.Entity.ID == workflow.Blocks[i].NextBlocks[j].ID);
                                if (!trackedNextBlock)
                                    dbContext.Blocks.Attach(workflow.Blocks[i].NextBlocks[j]);
                                else
                                {
                                    var id = workflow.Blocks[i].NextBlocks[j].ID;
                                    workflow.Blocks[i].NextBlocks[j] =
                                            dbContext.Blocks.FirstOrDefault(x => x.ID == id);
                                }
                            }

                        }

                        var javascriptId = workflow.Blocks[i].ID;
                        workflow.Blocks[i].ID = dbContext.Blocks.Add(workflow.Blocks[i]).ID;
                        foreach (var block in workflow.Blocks)
                        {
                            if (block.NextBlocks.Count > 0)
                            {
                                for (int j = 0; j < block.NextBlocks.Count; j++)
                                {
                                    if (block.NextBlocks[j].ID == javascriptId)
                                        block.NextBlocks[j] = workflow.Blocks[i];
                                }
                            }

                        }
                        dbContext.SaveChanges();
                    }
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

        public Workflow Update(Workflow newWorkflow, int oldWorkflowId, string userId)
        {

            try
            {
                var workflowAdded = Insert(newWorkflow, userId);
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var temp = dbContext.Workflows.FirstOrDefault(x => x.ID == oldWorkflowId);
                    if (temp != null) temp.IsActual = false;
                    dbContext.SaveChanges();
                    return workflowAdded;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem upravit workflow: {0}", e);
            }
        }


        public Workflow Delete(int idWorkflow)
        {
            try
            {
                using (var dbContext = new DBContextWFManagementSystem())
                {
                    var temp = dbContext.Workflows.FirstOrDefault(x => x.ID == idWorkflow);
                    if (temp != null) temp.IsActual = false;
                    dbContext.SaveChanges();
                    return temp;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Problem odebrat workflow: {0}", e);
            }
        }


    }
}
