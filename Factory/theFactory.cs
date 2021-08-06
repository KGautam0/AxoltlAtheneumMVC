using AxolotlAtheneum.BusinessLayer;
using AxolotlAtheneum.DataAccessLayer;
using AxolotlAtheneum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AxolotlAtheneum.Factory
{
    public class theFactory
    {
        public Object factory(int x)
        {
            switch (x)
            {

                case (1):
                    {
                        return new User();
                    }
                case (2):
                    {
                        return new Order();
                    }
                case (3):
                    {
                        return new User();
                    }
                case (4):
                    {
                        return new ShoppingBO();
                    }
                case (5):
                    {
                        return new userBO();
                    }
                case (6):
                    {
                        return new Random();
                    }
                case (7):
                    {
                        return new DAL();

                    }
                case (8):
                    {
                        return new EmailSender();

                    }
                case (9):
                    {
                        return new PaymentCard();

                    }

                case (10):
                    {
                        return new Book();

                    }
                case (11):
                    {
                        return new List<Book>();

                    }
                case (12):
                    {
                        return new List<User>();

                    }
                case (13):
                    {
                        return new ShoppingCart();

                    }

                case (14):
                    {
                        return new Order();

                    }


            }
            return null;
        }
    }
}