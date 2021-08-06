using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class User
    {
        public string userID { get; set; }

        //activation number
        public int actnum { get; set; }

        [DisplayName("First Name")]
        [Required]
        [MaxLength(length: 30)]
        public String firstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [MaxLength(length: 30)]
        public String lastName { get; set; }

        [DisplayName("Email")]
        [Required]
        [MaxLength(length: 50)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$")]
        public String email { get; set; }


        [DisplayName("Password")]
        [Required]
        [MaxLength(30)]
        public String password { get; set; }

        [DisplayName("Phone Number")]
        [Required]
        [MinLength(length: 10)]
        [MaxLength(length: 10)]
        [RegularExpression("^[0-9]*$")]
        public String phonenumber { get; set; }

        [DisplayName("Status")]
        public Status status { get; set; }

        [DisplayName("Subscription Status")]
        public int isSubscribed { get; set; }

        [DisplayName("Address")]
        public Address address { get; set; }

        [DisplayName("Payment Cards")]
        public List<PaymentCard> cards { get; set; }


    }
}