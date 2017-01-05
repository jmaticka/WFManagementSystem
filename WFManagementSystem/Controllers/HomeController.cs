using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using WFManagementSystem.Helpers;
using WFManagementSystem.ViewModels;
using WFMDatabase.DML;
using WFMDatabase.Entities;

namespace WFManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private IDMLField _fieldManager;
        private IDMLWorkflowInstance _instanceManager;

        public HomeController()
        {
            _fieldManager = new DMLField();
            _instanceManager = new DMLWorkflowInstances();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Akce u běžících procesů, které je třeba vykonat";
            var userId = User.Identity.GetUserId();
            ViewBag.Fields = _fieldManager.GetAllByWorker(userId);
            return View();
        }

        public ActionResult PerformAction(int id)
        {
            var field = _fieldManager.GetFiled(id);
            var fieldVm = new FieldViewModel
            {
                Block = field.Block,
                DateTimeEnded = field.DateTimeEnded,
                IsActive = field.IsActive,
                WorkflowInstance = field.WorkflowInstance,
                Worker = field.Worker,
                Action = field.Action,
                ID = field.ID
            };
            return View(fieldVm);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PerformAction(FieldViewModel field, HttpPostedFileBase file)
        {
            var helper = new ProcessHandler();
            var fieldToUpdate = _fieldManager.GetFiled(field.ID);
            //TODO stop process if not confirmed
            //vsechny fieldy isactive na false, nechat dateTimeEnded, u workflow instance dat dateTimeEnded na datetime.now
            if (field.StopWorkflow)
            {
                var fields = _fieldManager.GetAllByInstance(fieldToUpdate.WorkflowInstance.ID);
                var instance = _instanceManager.GetById(fieldToUpdate.WorkflowInstance.ID);
                instance.DateTimeEnded = DateTime.Now;
                _instanceManager.Update(instance);
                foreach (var fieldTemp in fields)
                {
                    fieldTemp.IsActive = false;

                    _fieldManager.Update(fieldTemp);
                }
                await helper.SendEmail(instance.UserStarted, fields.Last(),
                    "Workflow nedoběhlo do konce, bylo ukončeno uživatelem " + User.Identity.GetUserName());
                return RedirectToAction("Index", "Home");
            }


            if (file != null && file.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/"), file.FileName);
                file.SaveAs(path);
            }


            fieldToUpdate.Output = field.Output;
            _fieldManager.Update(fieldToUpdate);

            await helper.FinishTask(fieldToUpdate);
            return RedirectToAction("Index", "Home");
        }
    }
}