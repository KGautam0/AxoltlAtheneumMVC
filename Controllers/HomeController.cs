using AxolotlAtheneum.Models;
using System;
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
            User loggeduser = (User)Session["Logged_User"];
            if(loggeduser.status==3)
                    return View("AdminHomepage");
            return View("Homepage");
        }
        public ActionResult AdminHomepage()
        {
            return View();
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
    }
}