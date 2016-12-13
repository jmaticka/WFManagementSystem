using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WFManagementSystem.ViewModels;
using WFMDatabase.DML;

namespace WFManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private IDMLField _fieldManager;

        public HomeController()
        {
            _fieldManager = new DMLField();
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
            
            return View(_fieldManager.GetFiled(id));
        }
        
    }
}