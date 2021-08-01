using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class ShoppingCart
    {
        public int UserID { get; set; }
        public Dictionary<Book, int> Items { get; set; } 
        public double Total { get; set; }

    }
}