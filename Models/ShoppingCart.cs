using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class ShoppingCart
    {
        public int UserID { get; set; }
        public String PromoCode { get; set; }
        public Book[] Items { get; set; }
        public int[] Quantity { get; set; }
        public double Total { get; set; }

    }
}