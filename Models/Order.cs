using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class Order : ShoppingCart
    {
        public int OrderID { get; set; }
        public DateTime DateOrdered { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Address ShippingAddress { get; set; }
        public PaymentCard PaymentMethod { get; set; }

    }
}