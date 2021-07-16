using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class Customer
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "First name required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters", MinimumLength = 2)]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters", MinimumLength = 2)]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        public String Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(20, ErrorMessage = "2 to 20 characters", MinimumLength = 2)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public Status Status { get; set; }


    }
}