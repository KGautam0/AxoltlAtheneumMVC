﻿using AxolotlAtheneum.BusinessLayer;
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
            {
                return View(bo.getCart(loggeduser));
            }
            return View("LoginCheck");
        }

        public ActionResult Search()
        {
            List<Book> books = bo.getAllBooks();
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
            QueryCategory category;
            if (year.Checked) category = QueryCategory.Year;
            else if (author.Checked) category = QueryCategory.Author;
            else if (isbn.Checked) category = QueryCategory.ISBN;
            else if (publisher.Checked) category = QueryCategory.Publisher;
            else category = QueryCategory.Title;
            List<Book> books = bo.getFilteredBooks(searchParam, category);
            
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

        public ActionResult Book(Book book)
        {
            return View("Book", book);
        }

        public ActionResult AddToCart(Book book)
        {
            User loggeduser = (User)Session["Logged_User"];
            if (Session["Logged_User"] != null)
            {
                bo.addBookToCart(loggeduser, book);
                return View("Cart");
            }
            return View("LoginCheck");
        }
    }
}