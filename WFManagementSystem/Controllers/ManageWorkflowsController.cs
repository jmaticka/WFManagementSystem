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
            if (User.IsInRole("Admin")) ViewBag.Workflows = _workflowManager.GetAll();
            if (User.IsInRole("Garant"))
            {
                var userId = User.Identity.GetUserId();
                ViewBag.Workflows = _workflowManager.GetAllByUser(userId);
            }
            return View();
        }

        public ActionResult Create()
        {
            var users = UserManager.Users.ToList();
            var res = users;
            ViewBag.Users = res;

            ViewBag.BlockTypes = _blockTypeManager.GetAll();
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Users = UserManager.Users.ToList();
            ViewBag.BlockTypes = _blockTypeManager.GetAll();
            ViewBag.Workflow = _workflowManager.GetById(id);


            return View();
        }



        
    }
}