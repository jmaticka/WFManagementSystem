using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFManagementSystem.ViewModels;
using WFMDatabase;

namespace WFManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        DBContextWFManagementSystem test;
        public ActionResult Index()
        {
            ViewBag.Message = "Akce u běžících procesů, které je třeba vykonat";
            var testData = new List<ProcessStepViewModel>();

            testData.Add(new ViewModels.ProcessStepViewModel
            {
                Id = 1,
                Name = "Schvaleni ceny",
                Description = "Cena monitoru: 5000 Kč",
                StartedDate  = DateTime.Now
                        
            });
            testData.Add(new ViewModels.ProcessStepViewModel
            {
                Id = 2,
                Name = "Schvaleni ceny2",
                Description = "Cena monitoru: 6000 Kč",
                StartedDate = DateTime.Now
            });

            ViewBag.testData = testData;
            return View();
        }

        public ActionResult PerformAction(int id)
        {
            ViewBag.Field = new ProcessStepViewModel
            {
                Name = "Schvaleni ceny2",
                Description = "Cena monitoru: 6000 Kč",
                StartedDate = DateTime.Now
            };
            return View();
        }
        
    }
}