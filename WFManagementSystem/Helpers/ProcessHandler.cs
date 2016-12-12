using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using WFMDatabase.DML;
using WFMDatabase.Entities;

namespace WFManagementSystem.Helpers
{
    public class ProcessHandler
    {
        private IDMLField _fieldManager;

        public ProcessHandler()
        {
            _fieldManager = new DMLField();
        }
        public async Task<bool> ProcessTheField(Field fieldToProcess)
        {
            if (fieldToProcess == null)
            {
                return true;
            }
            var result = _fieldManager.GetFiled(fieldToProcess);
            if (result.Block.BlockType.Name == "start" || result.Block.BlockType.Name == "konec" || result.Block.BlockType.Name == "rozdělení")
            {
                result.IsActive = false;
                await Task.Delay(1000);
                result.DateTimeEnded = DateTime.Now;
                _fieldManager.Update(result);

                if (result.Block.BlockType.Name == "rozdělení")
                {
                    foreach (var nextBlock in result.Block.NextBlocks)
                    {
                        var toProcess = MoveToNextField(nextBlock, result.WorkflowInstance.ID);
                        await ProcessTheField(toProcess);
                    }
                    return true;
                }
                if(result.Block.BlockType.Name == "sloučení")
                {
                    var fields = _fieldManager.GetAllByInstance(fieldToProcess.WorkflowInstance.ID);

                    if (fields.Any(field => field.Block.Position != null && field.IsActive))
                    {
                        result.IsActive = true;
                        _fieldManager.Update(result);
                        return false;
                    }

                    var nextField = MoveToNextField(result);
                    await ProcessTheField(nextField);
                    return true;
                }
                else
                {
                    var nextField = MoveToNextField(result);
                    await ProcessTheField(nextField);
                    return true;
                }


            }

            result.IsActive = true;
            _fieldManager.Update(result);

            if (fieldToProcess.Worker != null)
            {
                //await SendEmail(result.Worker, result);
            }
            return true;
        }

        public Field MoveToNextField(Field field)
        {
            var result = _fieldManager.GetFieldSuccessorByBlockId(field.Block.NextBlocks.First().ID, field.WorkflowInstance.ID);
            return result;
        }
        public Field MoveToNextField(Block block, int workflowInstanceId)
        {
            var result = _fieldManager.GetFieldSuccessorByBlockId(block.ID, workflowInstanceId);
            return result;
        }
        public async Task<bool> SendEmail(ApplicationUser user, Field field)
        {

            var message = new MailMessage();
            message.To.Add(new MailAddress(user.Email)); // replace with valid value 
            message.From = new MailAddress("workflowupce@gmail.com"); // replace with valid value
            message.Subject = $"Workflow proces {field.Action}";
            message.Body = field.Action;
            message.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "workflowupce@gmail.com", // replace with valid value
                        Password = "Workflow1." // replace with valid value
                    };

                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}