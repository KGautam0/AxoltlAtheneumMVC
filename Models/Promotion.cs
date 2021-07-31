using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class Promotion
    {
        public String PromoCode { get; set; }
        public String PromoName { get; set; }
        public float ValueOff { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}