using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxolotlAtheneum.Controllers
{
    public class AccountsController : Controller
    {
        public ActionResult Index()
        {
            return Login();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        public ActionResult OrderHistory()
        {
            return View();
        }
        public ActionResult EditProfile()
        {
            return View();
        }
    }
}