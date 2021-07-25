using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    [Serializable]
    public class PaymentCard
    {
        [Required]
        [RegularExpression(@"^\d{16}$")]
        public String cardNumber { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}$")]
        public String cardSecNumber { get; set; }

        [Required]
        
        public CardType cardType { get; set; }


        
        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])$")]
        public String expMonth {get; set;}

        [Required]
        [RegularExpression(@"^20[0-9]{2}$")]
        public String expYear { get; set; }
    }
}