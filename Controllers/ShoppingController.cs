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

        public ActionResult Author3()
        {
            return Query("Shikibu, Murasaki", null, "on", null, null, null);
        }

        public ActionResult Author2()
        {
            return Query("Stoker, Bram", null, "on", null, null, null);
        }

        public ActionResult addPromo(string addPromoName, string addPromoCode, string addPromoDiscount)
        {
            if (addPromoName != null && addPromoCode != null && addPromoDiscount != null)
            {
                bo.addPromo(addPromoName, addPromoCode, addPromoDiscount);
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

        public ActionResult Checkout()
        {
            User loggeduser = (User)Session["Logged_User"];
            return View("Checkout", loggeduser);
        }

        public ActionResult ConfirmOrder(ShoppingCart cart)
        {
            return View("OrderConfirmation");
        }

        public ActionResult CheckoutItems()
        {
            User loggeduser = (User)Session["Logged_User"];
            return PartialView("_CheckoutItems", bo.getCart(loggeduser));
        }

        public ActionResult SelectCard(string card0, string card1, string card2)
        {
            User user = (User)Session["Logged_User"];
            ShoppingCart cart = bo.getCart(user);
            Order order = new Order();
            order.DateOrdered = new DateTime();
            order.OrderStatus = OrderStatus.Pending;
            order.OrderID = new Random().Next(5000);
            order.ShippingAddress = user.address;
            order.userID = int.Parse(user.userID);
            order.price = cart.Total;
            order.Items = cart.Items;
            if (card2 != null && card2.Equals("on"))
                order.PaymentMethod = user.cards[2];
            else if (card1 != null && card1.Equals("on"))
                order.PaymentMethod = user.cards[1];
            else order.PaymentMethod = user.cards[0];
            bo.insertOrder(order);
            return View("OrderConfirmation", order);
        }

    }
}