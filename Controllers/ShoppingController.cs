using AxolotlAtheneum.BusinessLayer;
using AxolotlAtheneum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AxolotlAtheneum.Controllers
{
    public class ShoppingController : Controller
    {
        ShoppingBO bo = new ShoppingBO();
        public ActionResult Index()
        {
            return View("Search");
        }
        public ActionResult OrderHistory()
        {
            if (Session["Logged_User"] != null)
                return View();
            return View("LoginCheck");
        }

        public ActionResult Cart()
        {
            User loggeduser = (User)Session["Logged_User"];
            if (Session["Logged_User"] != null)
                return View(this);
            return View("LoginCheck", loggeduser);
        }

        public ActionResult Search()
        {
            List<Book> books = ShoppingBO.getAllBooks();
            return View(books);
        }

        public ActionResult ManageBooks()
        {
            //User loggeduser = (User)Session["Logged_User"];
            //if (Session["Logged_User"] != null && loggeduser.status.Equals(Status.Admin))
                return View();
            //return View("LoginCheck");
        }

        public ActionResult Query(String searchParam, RadioButton title, RadioButton author, RadioButton isbn, RadioButton publisher, RadioButton year)
        { 
            string category;
            if (year.Checked) category = year.ID;
            else if (author.Checked) category = author.ID;
            else if (isbn.Checked) category = isbn.ID;
            else if (publisher.Checked) category = publisher.ID;
            else category = title.ID;
            List<Book> books = ShoppingBO.getFilteredBooks(searchParam, category);
            
            return View("Search", books);
                
        }

        public ActionResult addPromo(string promoName, string promoCode, string discount)
        {
            if (promoName != null && promoCode != null && discount != null)
            {
                bo.addPromo(promoName, promoCode, discount);
            }
            return View("Homepage", "Account");
        }

        public ActionResult AddBook(string isbn, string category, string author, string title, string edition, string publisher, string year, string quantity, string threshold, string buyingPrice, string sellingPrice, string coverpicture)
        {
            Book book = bo.addBook(isbn, category, author, title, edition, publisher, year, quantity, threshold, buyingPrice, sellingPrice, coverpicture);
            return View("Book", book);
        }

        public ActionResult DeeletBook(string isbn)
        {
            return View("AdminHomepage");
        }
    }
}