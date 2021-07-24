using AxolotlAtheneum.BusinessLayer;
using AxolotlAtheneum.Models;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace AxolotlAtheneum.Controllers
{
    public class AccountController : Controller
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
                User logged_user = USERBO.logUSER(email, password);
                if (logged_user == null)
                {
                    return View("LoginFail");
                }


                Session["Logged_User"] = logged_user;
                return RedirectToAction("Index", "Home");
            }


            return View("LoginInv");

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

        public ActionResult EditAccount()
        {
            return View();
        }
       
        public ActionResult editName(String newFName, String newLName)
        {
            User loggeduser = (User)Session["Logged_User"];
            if(newFName != null)
                loggeduser.firstName = newFName;
            if(newLName != null)
                loggeduser.lastName = newLName;

            Session["Logged_User"] = USERBO.updateUSER(loggeduser);

            return RedirectToAction("EditAccount");
        }


        [HttpPost]
        public ActionResult editAddress(Address newAddress)
        {
            User loggeduser = (User)Session["Logged_User"];
            if (newAddress != null)
                loggeduser.address = newAddress;
            

            USERBO.updateUSER(loggeduser);

            return RedirectToAction("EditAccount");
        }
        [HttpPost]
        public ActionResult editPhone(String newPhone)
        {
            User loggeduser = (User)Session["Logged_User"];
            if (newPhone != null)
                loggeduser.phonenumber = newPhone;
           

            USERBO.updateUSER(loggeduser);

            return RedirectToAction("EditAccount");
        }
        [HttpPost]
        public ActionResult verUser(int regCode)
        {
            User loggeduser = (User)Session["Logged_User"];
            if (loggeduser.actnum == regCode)
            {
                loggeduser.status = 2;
                USERBO.updateUSER(loggeduser);
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