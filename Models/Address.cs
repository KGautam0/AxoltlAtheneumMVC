using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    [Serializable]
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }

        public State state { get; set; }
        public int Zip { get; set; }

    }
}