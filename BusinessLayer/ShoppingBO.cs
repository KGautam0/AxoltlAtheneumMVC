using AxolotlAtheneum.Models;
using AxolotlAtheneum.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.BusinessLayer
{
    public class ShoppingBO
    {
        DAL USERDAL = new DAL();

        public List<Book> getFilteredBooks(string query, QueryCategory category)
        {
            List<Book> books = new List<Book>();
            books = USERDAL.filterBooks(query, category.ToString());
            return books;
        }

        public void addPromo(string name, string code, string amt)
        {

        }

        public Book addBook(string isbn, string category, string author, string title, string edition, string publisher, string year, string quantity, string threshold, 
                string buyingPrice, string sellingPrice, string cover)
        {
            Book book = new Book();
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

        public void insertOrder(Order order)
        {
            USERDAL.insertOrder(order);
        }


    }
}