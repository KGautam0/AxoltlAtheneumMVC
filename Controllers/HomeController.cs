﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxolotlAtheneum.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Homepage");
        }
        public ActionResult Homepage()
        {
            

            return Index();
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

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Book()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }
    }
}