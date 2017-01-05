using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WFMDatabase.DML;
using WFMDatabase.Entities;

namespace WFManagementSystem.Helpers
{
    public class ProcessHandler
    {
        private IDMLField _fieldManager;
        private IDMLWorkflowInstance _instaceManager;

        public ProcessHandler()
        {
            _fieldManager = new DMLField();
            _instaceManager = new DMLWorkflowInstances();
        }
        public async Task<bool> ProcessTheField(Field fieldToProcess)
        {
            if (fieldToProcess == null)
            {
                return true;
            }
            var result = _fieldManager.GetFiled(fieldToProcess.ID);

            if (result.Worker != null)
            {
                await SendEmail(result.Worker, result);
            }
            if (result.Block.BlockType.Name == "start" || result.Block.BlockType.Name == "konec" ||
                result.Block.BlockType.Name == "rozdělení" || result.Block.BlockType.Name == "sloučení" ||
                result.Block.BlockType.Name == "api")
            {
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
                    if (result.Block.BlockType.Name == "api")
                    {
                        var fields = _fieldManager.GetAllByInstance(fieldToProcess.WorkflowInstance.ID);
                        var fileField = fields.FirstOrDefault(x => x.Block.BlockType.Name == "soubor");
                        if (fileField != null)
                        {
                            double currencyResult;
                            try
                            {
                                currencyResult = ContactApi(Convert.ToSingle(fileField.Output));
                            }
                            catch (Exception)
                            {

                            }
                            currencyResult = Convert.ToSingle(fileField.Output)/27.0200;
                            result.Output = currencyResult.ToString(CultureInfo.InvariantCulture);
                            _fieldManager.Update(result);
           
                            if (currencyResult > 5000/27.020)
                            {
                                var decideField = fields.FirstOrDefault(x => x.Block.Name == "Rozhodnutí");
                                _fieldManager.UpdateUser(decideField);

                            }
                        }

                        var nextField = MoveToNextField(result);
                        await ProcessTheField(nextField);
                        return true;

                    }
                    if (result.Block.BlockType.Name == "konec")
                    {
                        var instance = _instaceManager.GetById(result.WorkflowInstance.ID);
                        await SendEmail(instance.UserStarted, result, instance.Workflow.Name + " Workflow  úspěšně dokončeno");
                        return true;
                        
                    }
                    if (result.Block.BlockType.Name == "sloučení")
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
            }
            
            result.IsActive = true;
            _fieldManager.Update(result);

            
            return true;
        }

        public async Task<bool> FinishTask(Field fieldToProcess)
        {
            var result = _fieldManager.GetFiled(fieldToProcess.ID);

            result.IsActive = false;
            await Task.Delay(1000);
            result.DateTimeEnded = DateTime.Now;

            _fieldManager.Update(result);

            var nextField = MoveToNextField(result);
            await ProcessTheField(nextField);
            return true;
        }

        public Field MoveToNextField(Field field)
        {
            if(field.Block.NextBlocks == null)
            { 
                return null;
            }
            var result = _fieldManager.GetFieldSuccessorByBlockId(field.Block.NextBlocks.First().ID, field.WorkflowInstance.ID);
            return result;
        }
        public Field MoveToNextField(Block block, int workflowInstanceId)
        {
            var result = _fieldManager.GetFieldSuccessorByBlockId(block.ID, workflowInstanceId);
            return result;
        }

        public double ContactApi(float number)
        {
            CurrencyAPI api = new CurrencyAPI();
            api.prepareEnvelope(number);
            var result = api.sendRequest();
            return result;
        }
        public async Task<bool> SendEmail(ApplicationUser user, Field field, string text="")
        {

            var message = new MailMessage();
            message.To.Add(new MailAddress(user.Email)); // replace with valid value 
            message.From = new MailAddress("workflowupce@gmail.com"); // replace with valid value
            message.Subject = $"Workflow proces {field.Action}";
            message.Body = "Přihlašte se do systému Workflow, čeká na Vás akce: "+text + field.Action;
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