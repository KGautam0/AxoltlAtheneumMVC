using AxolotlAtheneum.BusinessLayer;
using AxolotlAtheneum.Models;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace AxolotlAtheneum.Controllers
{
    public class LoginController : Controller
    {
        userBO USERBO = new userBO();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult logUser(String email, String password)
        {
            Regex regex = new Regex("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");

            if ((regex.IsMatch(email)) & (password.Length <= 30))
            {
                User logged_user = USERBO.logUSER("email", "password");
                if (logged_user == null)
                {
                    return View();
                }
            }
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



        public ActionResult verUser(int x)
        {
            User loggeduser = (User)Session["Logged_User"];
            if (loggeduser.actnum == x)
            {
                loggeduser.status = 2;
                return Index();
            }
            return View("verificationFail");
        }

        public ActionResult regUser(User x)
        {
            if (ModelState.IsValid)
            {

                x.status = 1;
                x.actnum = new Random().Next(1000);
                USERBO.regUSER(x);

                Session["Logged_User"] = x;



                return View("regSucc");
            }


            return View("RegistrationFail");
        }
    }
}