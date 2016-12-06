using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using WFManagementSystem.ViewModels;
using WFMDatabase;
using WFMDatabase.DML;
using WFMDatabase.Entities;

namespace WFManagementSystem.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ProcessController : Controller
    {
        private IDMLWorkflow _workflowManager;
        private DMLWorkflowInstances _workflowInstancesManager;
        private DMLBlock _workflowBlockManager;

        public ProcessController()
        {
            _workflowManager = new DMLWorkflow();
            _workflowInstancesManager = new DMLWorkflowInstances();
            _workflowBlockManager= new DMLBlock();
            
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        // GET: Process
        public ActionResult Index()
        {
            if (User.IsInRole("Admin")) ViewBag.WorkflowInstances = _workflowInstancesManager.GetAll();
            if (User.IsInRole("Garant"))
            {
                var userId = User.Identity.GetUserId();
                ViewBag.WorkflowInstances = _workflowInstancesManager.GetAllByUser(userId);
            }
            return View();
        }

        // GET: Process/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Field field=null;
            if (field == null)
            {
                return HttpNotFound();
            }
            return View(field);
        }

        // GET: Process/Create
        public ActionResult Create(int id)
        {
            var workflow = _workflowManager.GetById(id);

            ProcessViewModel model = new ProcessViewModel
            {
                WorkflowId = id,
                WorkflowName = workflow.Name,
                Fields = workflow.Blocks.Select(x=> new FieldBlockViewModel
                {
                    BlockId = x.ID,
                    Name = x.Name,
                    Description = x.Description,
                    Worker = x.Worker,
                    Position = x.Position,
                    BlockType = x.BlockType
                }).ToList()
            };



            var users = UserManager.Users.ToList();
            ViewBag.Users = users;
            return View(model);
        }

        // POST: Process/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProcessViewModel vm)
        {
            var test = vm;

            return RedirectToAction("Index");
        }

        public ActionResult CreateProcess()
        {
            if (User.IsInRole("Admin")) ViewBag.Workflows = _workflowManager.GetAll();
            if (User.IsInRole("Garant"))
            {
                var userId = User.Identity.GetUserId();
                ViewBag.Workflows = _workflowManager.GetAllByUser(userId);
            }
            return View();
        }
    }
}
