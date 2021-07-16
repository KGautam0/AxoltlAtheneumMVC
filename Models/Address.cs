using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class Address
    {
        public String Street { get; set; }
        public String City { get; set; }
        public int Zip { get; set; }
        public State State { get; set; }
    }
}