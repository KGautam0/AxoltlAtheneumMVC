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

        [ChildActionOnly]
        public ActionResult rendereditAddress()
        {
            User loggeduser = (User)Session["Logged_User"];
            return PartialView("_editAddress",loggeduser.address);
        }

        [ChildActionOnly]
        public ActionResult rendereditCard()
        {
            User loggeduser = (User)Session["Logged_User"];
            return PartialView("_editCard", loggeduser.card);
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
            User loggeduser = (User)Session["Logged_User"];
            if(loggeduser == null)
            return View("Registration");
            if (loggeduser.status == 1)
            {
                return View("regSucc");
            }

            return View("Registration");


        }

        public ActionResult EditAccount()
        {
            User loggeduser = (User)Session["Logged_User"];
            if (Session["Logged_User"] != null)
            return View(loggeduser);
            return View("LoginCheck");
        }
       
        public ActionResult editName(String newFName, String newLName)
        {
            User loggeduser = (User)Session["Logged_User"];
            if (newFName != null)
                loggeduser.firstName = newFName;
            if(newLName != null)
                loggeduser.lastName = newLName;

            Session["Logged_User"] = USERBO.updateUSER(loggeduser);

            return RedirectToAction("EditAccount",loggeduser);
        }


        [HttpPost]
        public ActionResult editAddress(Address newAddress)
        {
            User loggeduser = (User)Session["Logged_User"];
            if (!ModelState.IsValid)
                return View("EditAccFail",loggeduser);
            loggeduser.address = newAddress;
            

            USERBO.updateUSER(loggeduser);

            return RedirectToAction("EditAccount",loggeduser);
        }
        [HttpPost]
        public ActionResult editCard(PaymentCard newCard)
        {
            User loggeduser = (User)Session["Logged_User"];
            if (!ModelState.IsValid)
                return View("EditAccFail", loggeduser);
            loggeduser.card = newCard;


            USERBO.updateUSER(loggeduser);

            return RedirectToAction("EditAccount", loggeduser);
        }
        [HttpPost]
        public ActionResult editPhone(String newPhone)
        {
            User loggeduser = (User)Session["Logged_User"];
            int z;



            if (!int.TryParse(newPhone, out z) | (newPhone == null))
                return View("EditAccFail", loggeduser);

            loggeduser.phonenumber = newPhone;
            USERBO.updateUSER(loggeduser);

            return RedirectToAction("EditAccount", loggeduser);
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