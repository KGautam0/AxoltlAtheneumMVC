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
            if (loggeduser == null)
                return View("Homepage", loggeduser);
            if(loggeduser.status.Equals(Status.Admin))
                    return View("AdminHomepage", loggeduser);
            return View("Homepage", loggeduser);
        }
        public ActionResult AdminHomepage()
        {
            User loggeduser = (User)Session["Logged_User"];
            if (Session["Logged_User"] != null)
                return View();
            return View("LoginCheck");
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