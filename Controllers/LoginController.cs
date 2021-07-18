using AxolotlAtheneum.BusinessLayer;
using AxolotlAtheneum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AxolotlAtheneum.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return Index();
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult regPage()
        {
            return View("Registration");
        }

        
        public ActionResult regUser(User x)
        {
            if (ModelState.IsValid)
            {
                userBO USERBO = new userBO();
                x.status = 1;

                USERBO.regUSER(x);

                Session["Logged_User"] = x;



                return View("regSucc");
            }
            

            return View("RegistrationFail");
        }
    }
}