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
        public ActionResult logUser(String email, String password, String userID)
        {
            Regex regex = new Regex("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");

            if (((regex.IsMatch(email))  |( (userID.Length <= 6 ) & (userID.Length <=0))) & ((password.Length <= 30) & (password.Length>0)))
            {
                User logged_user = null;
                if (email == null)
                logged_user= USERBO.IDlogUSER(userID, password);
                if(userID == "")
                logged_user = USERBO.EMlogUSER(email, password);
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
        public ActionResult verUser(Nullable <int> regCode)
        {
            if(regCode == null)
                return View("verificationFail");
            User loggeduser = (User)Session["Logged_User"];
            if (loggeduser.actnum == regCode)
            {
                loggeduser.status = 2;
                
                loggeduser.userID = new Random().Next(5000).ToString();
                USERBO.verUSER(loggeduser);
                USERBO.updateUSER(loggeduser);
                return View("Index");
            }
            return View("verificationFail");
        }


        public ActionResult ResetPassword()
        {

            return View();

        }
        public ActionResult sendresetPassEmail(String email)
        {
            if (!USERBO.checkUSER(email))
            {
                User loggeduser = (User)Session["Logged_User"];
                loggeduser.actnum = new Random().Next(1000);
                USERBO.resetPass(email, loggeduser.actnum);

                return View("ResetPasswordSub");
            }
            
            
            
            return RedirectToAction("Index", "Home");

        }
        public ActionResult changePassword(int regnum, String pass)
        {
            User loggeduser = (User)Session["Logged_User"];
            if ((loggeduser.actnum == regnum) & pass.Length <31)
            {
                loggeduser.password = pass;

                return View("ResetPasswordSucc");
            }
            return View("ResetPasswordFail");

        }
        public ActionResult regUser(User x)
        {
            if (ModelState.IsValid)
            {

                if (USERBO.checkUSER(x.email))
                { return View("RegistrationDupe"); }
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