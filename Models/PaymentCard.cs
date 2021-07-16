using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class PaymentCard
    {
        public int CardNumber { get; set; }
        public CardType CardType { get; set; }
        public DateTime ExpDate { get; set; }

    }
}