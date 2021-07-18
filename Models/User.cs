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
        public int userID { get; set; }

        public int actnum { get; set; }

        [DisplayName("First Name")]
        [Required]
        [MaxLength(30)]
        public String firstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        [MaxLength(30)]
        public String lastName { get; set; }

        [DisplayName("Email")]
        [Required]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public String email { get; set; }


        [DisplayName("Password")]
        [Required]
        [MaxLength(30)]
        public String password { get; set; }

        [DisplayName("Status")]
        public int status { get; set; }

        [DisplayName("Admin Status")]
        public bool isAdmin { get; set; }


        [DisplayName("Address")]
        public Address address { get; set; }

        [DisplayName("Payment Card")]
        public PaymentCard card { get; set; }




        //0-3 is suspended -> inactive -> active ->admin
        public void promoteUser(User x, int newRank)
        {
            if (((this.isAdmin == true) & (newRank >= 0)) & (newRank <= 4))
            {
                x.status = newRank;
            }

        }
        public void demoteUser(User x, int newRank)
        {
            if (((this.isAdmin == true) & (newRank >= 0)) & (newRank <= 4))
            {
                x.status = newRank;
            }
        }


        public void suspendUser(User x)
        {
            if (this.isAdmin == true)
            {
                x.status = 0;
            }
        }
    }
}