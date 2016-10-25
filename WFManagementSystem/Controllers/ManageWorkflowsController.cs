using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFMDatabase.DML;

namespace WFManagementSystem.Controllers
{
    [Authorize]
    public class ManageWorkflowsController : Controller
    {
        private IDMLWorkflow _workflowManager;

        public ManageWorkflowsController()
        {
            _workflowManager = new DMLWorkflow();
        }
       
        // GET: ManageWorkflows
        public ActionResult Index()
        {
            if (User.IsInRole("admin")) ViewBag.Workflows = _workflowManager.GetAll();
            if (User.IsInRole("garant"))
            {
                var userId = User.Identity.GetUserId();
                ViewBag.Workflows = _workflowManager.GetAllByUser(userId);
            }
            return View();
        }
    }
}