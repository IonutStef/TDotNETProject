﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LectorASPAutoGenerated.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataBase()
        {
            return View();
        }

        /*
        public ActionResult DataBase(string optionSelected)
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            List<ProjectTSDotNETServiceReference.Subject> subjects = lectorSrv.GetSubjects("").ToList();
            ViewData["Subjects"] = subjects;
            string valueReturned = "Subjects";
            return View(valueReturned);
        }
        */

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