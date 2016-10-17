﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFMDatabase;

namespace WFManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        DBContextWFManagementSystem test;
        public ActionResult Index()
        {
            test = new DBContextWFManagementSystem();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}