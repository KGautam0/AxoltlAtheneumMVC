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
        public ActionResult Index()
        {
            return View("Search");
        }
        public ActionResult OrderHistory()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Search()
        {
            List<Book> books = ShoppingBO.getAllBooks();
            return View(books);
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
    }
}