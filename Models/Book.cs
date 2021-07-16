using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class Book
    {
        public int ISBN { get; set; }
        public String Category { get; set; }
        public String Title { get; set; }
        public int Edition { get; set; }
        public String Author { get; set; }
        public String Publisher { get; set; }
        public int PublicationYear { get; set; }
        public String CoverPictureURL { get; set; }
        public String Description { get; set; }
        public double Price { get; set; }
        public float Rating { get; set; }
        public int QuantityInStock { get; set; }
        public int MinimumThreshold { get; set; }
        public double BuyingPrice { get; set; }
        public double SellingPrice { get; set; }
    }
}