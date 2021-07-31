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
        userDAL USERDAL = new userDAL();
        public static List<Book> getAllBooks()
        {
            List<Book> books = new List<Book>();
            return books;
        }

        public static List<Book> getFilteredBooks(string query, string category)
        {
            List<Book> books = new List<Book>();
            return books;
        }
    }
}