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
                case(7):
                    {
                        return new DAL();

                    }
                case (8):
                    {
                        return new EmailSender();

                    }
                    
                       
            }
                    
                return null;
         }
    }
}