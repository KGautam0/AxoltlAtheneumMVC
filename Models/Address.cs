using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    [Serializable]
    public class Address
    {
        public string Street { get; set; }

        public string Street2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
        public string Zip { get; set; }

    }
}