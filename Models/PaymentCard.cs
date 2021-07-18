using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    [Serializable]
    public class PaymentCard
    {
        public int cardNumber { get; set; }

        public CardType cardType { get; set; }

        public DateTime expDate { get; set; }


    }
}