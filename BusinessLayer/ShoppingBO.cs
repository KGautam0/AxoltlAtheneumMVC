using AxolotlAtheneum.Models;
using AxolotlAtheneum.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AxolotlAtheneum.Factory;

namespace AxolotlAtheneum.BusinessLayer
{
    public class ShoppingBO
    {
        DAL USERDAL = (DAL)new theFactory().factory(7);

        public List<Book> getFilteredBooks(string query, QueryCategory category)
        {
            List<Book> books = (List<Book>)new theFactory().factory(11);
            books = USERDAL.filterBooks(query, category.ToString());
            return books;
        }

        public void addPromo(string name, string code, string amt)
        {
            EmailSender messenger = new EmailSender();
            List<User> subscribers = USERDAL.getSubscribedUsers();
            foreach (User user in subscribers)
                messenger.sendPromo(user.email, code, amt);
        }

        public Book addBook(string isbn, string category, string author, string title, string edition, string publisher, string year, string quantity, string threshold,
                string buyingPrice, string sellingPrice, string cover)
        {
            Book book = (Book)new theFactory().factory(10);
            book.ISBN = int.Parse(isbn);
            book.Category = category;
            book.Author = author;
            book.Title = title;
            book.Edition = int.Parse(edition);
            book.Publisher = publisher;
            book.PublicationYear = int.Parse(year);
            book.QuantityInStock = int.Parse(quantity);
            book.MinimumThreshold = int.Parse(threshold);
            book.BuyingPrice = double.Parse(buyingPrice);
            book.SellingPrice = double.Parse(sellingPrice);
            book.CoverPictureURL = cover;
            USERDAL.insertBook(book);

            return book;
        }

        public List<Book> getAllBooks()
        {
            List<Book> books = USERDAL.getAllBooks();
            return books;
        }

        public ShoppingCart addBookToCart(User user, Book book)
        {
            return USERDAL.addBookToCart(user, book.ISBN, 1, book.SellingPrice);
        }

        public ShoppingCart getCart(User user)
        {
            return USERDAL.getCart(user);
        }




        public ShoppingCart removeFromCart(User user, Book book)
        {
            return USERDAL.removeFromCart(user, book);
        }

       public Promotion getPromo(string promoCode)
        {
            return USERDAL.getPromo(promoCode);
        }


        public void confORDER(User x)
        {

            EmailSender messenger = (EmailSender)new theFactory().factory(8);
            messenger.sendCartConfirmation(x);

        }

    }
}