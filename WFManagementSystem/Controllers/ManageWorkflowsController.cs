using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFMDatabase.DML;
using WFMDatabase.Entities;

namespace WFManagementSystem.Controllers
{
    [Authorize]
    public class ManageWorkflowsController : Controller
    {
        private IDMLWorkflow _workflowManager;
        private IDMLBlockType _blockTypeManager;

        public ManageWorkflowsController()
        {
            _workflowManager = new DMLWorkflow();
            _blockTypeManager = new DMLBlockType();
           
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
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

        public ActionResult Create()
        {
            var users = UserManager.Users.Select(x => new {x.Id, x.UserName}).ToList();
            var res = new SelectList(users, "Id", "UserName");
            ViewBag.Users = res;

            ViewBag.BlockTypes = new SelectList(_blockTypeManager.GetAll(), "ID", "Name");
            return View();
        }


        
    }
}