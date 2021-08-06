using AxolotlAtheneum.BusinessLayer;
using AxolotlAtheneum.Factory;
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
        ShoppingBO bo = (ShoppingBO)new theFactory().factory(4);
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
            User loggeduser = (User)Session["Logged_User"];
            if (Session["Logged_User"] != null && loggeduser.status.Equals(Status.Admin))
                return View();
            return View("LoginCheck");
        }

        public ActionResult Query(String searchParam, string title, string author, string isbn, string publisher, string category)
        {
            QueryCategory qCategory;
            if (category != null && category.Equals("on")) qCategory = QueryCategory.Category;
            else if (author != null && author.Equals("on")) qCategory = QueryCategory.Author;
            else if (isbn != null && isbn.Equals("on")) qCategory = QueryCategory.ISBN;
            else if (publisher != null && publisher.Equals("on")) qCategory = QueryCategory.Publisher;
            else qCategory = QueryCategory.Title;
            List<Book> books = bo.getFilteredBooks(searchParam, qCategory);

            return View("Search", books);

        }

        public ActionResult Nonfiction()
        {
            return Query("Nonfiction", null, null, null, null, "on");
        }

        public ActionResult Fiction()
        {
            return Query("Fiction", null, null, null, null, "on");
        }

        public ActionResult Classics()
        {
            return Query("Classics", null, null, null, null, "on");
        }

        public ActionResult Poems()
        {
            return Query("Poems", null, null, null, null, "on");
        }

        public ActionResult Historical()
        {
            return Query("Historical", null, null, null, null, "on");
        }
        public ActionResult Romance()
        {
            return Query("Romance", null, null, null, null, "on");
        }

        public ActionResult Feature1()
        {
            Book book = bo.getFilteredBooks("12346", QueryCategory.ISBN)[0];
            return Book(book);
        }

        public ActionResult Feature2()
        {
            Book book = bo.getFilteredBooks("97805", QueryCategory.ISBN)[0];
            return Book(book);
        }

        public ActionResult Feature3()
        {
            Book book = bo.getFilteredBooks("439708184", QueryCategory.ISBN)[0];
            return Book(book);
        }

        public ActionResult Top1()
        {
            Book book = bo.getFilteredBooks("18571", QueryCategory.ISBN)[0];
            return Book(book);
        }

        public ActionResult Top2()
        {
            Book book = bo.getFilteredBooks("97872", QueryCategory.ISBN)[0];
            return Book(book);
        }

        public ActionResult Top3()
        {
            Book book = bo.getFilteredBooks("818404397", QueryCategory.ISBN)[0];
            return Book(book);
        }

        public ActionResult Author1()
        {
            return Query("McDougal, Holt", null, "on", null, null, null);
        }

        public ActionResult Author2()
        {
            return Query("Shikibu, Murasaki", null, "on", null, null, null);
        }

        public ActionResult Author3()
        {
            return Query("Stoker, Bram", null, "on", null, null, null);
        }

        public ActionResult addPromo(string addPromoName, string addPromoCode, double addPromoDiscount, DateTime addStart, DateTime addEnd)
        {
            if (addPromoName != null && addPromoCode != null && addPromoDiscount != null && addStart != null && addEnd != null)
            {
                bo.addPromo(addPromoName, addPromoCode, addPromoDiscount, addStart, addEnd);
            }
            return View("Homepage", "Account");
        }

        public ActionResult AddBook(string isbn, string category, string author, string title, string edition, string publisher, string year, string quantity, string threshold, string buyingPrice, string sellingPrice, string coverpicture)
        {
            Book book = bo.addBook(isbn, category, author, title, edition, publisher, year, quantity, threshold, buyingPrice, sellingPrice, coverpicture);
            return View("Book", book);
        }

        public ActionResult DeleteBook(string isbn)
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
                ShoppingCart cart = bo.addBookToCart(loggeduser, book);
                return View("Cart", cart);
            }
            return View("LoginCheck");
        }

        public ActionResult RemoveFromCart(Book book)
        {
            User loggeduser = (User)Session["Logged_User"];
            ShoppingCart cart = bo.removeFromCart(loggeduser, book);
            return View("Cart", cart);
        }

        public ActionResult Checkout(ShoppingCart cart)
        {
            User loggeduser = (User)Session["Logged_User"];
            cart = bo.getCart(loggeduser);
            Order order = (Order) new theFactory().factory(14);
            if(loggeduser.address !=null)
            order.ShippingAddress = loggeduser.address;
            if(loggeduser.cards.Count != 0)
            order.PaymentMethod = loggeduser.cards[0];
            order.price = cart.Total;
            order.Items = cart.Items;
            return View("Checkout", order);
        }

        public ActionResult Confirm(ShoppingCart cart)
        {
            User loggeduser = (User)Session["Logged_User"];
            bo.confORDER(loggeduser);
            return View("OrderConfirmation");
        }

        public ActionResult userAddPromo(string promoCode)
        {
            Promotion promo = bo.getPromo(promoCode);
            if (promo != null)
            {
                User loggeduser = (User)Session["Logged_User"];
                bo.getCart(loggeduser).Discount = promo.ValueOff;
            }
            return RedirectToAction("Cart"); ;
        }

    }
}