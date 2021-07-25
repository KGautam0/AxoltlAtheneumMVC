﻿using AxolotlAtheneum.DataAccessLayer;
using AxolotlAtheneum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.BusinessLayer
{
    public class userBO
    {
        userDAL USERDAL = new userDAL();
        public void regUSER(User x)
        {
           
            if (USERDAL.insertUSER(x))
            {
                EmailSender messenger = new EmailSender();
                messenger.sendACTNUM(x);
            }
        }

        public User logUSER(String email, String password)
        {
            List<User> retrievedUser = USERDAL.retrieveUSER(email, password);
            if (retrievedUser.ElementAt(0) != null)
            {
                return retrievedUser.ElementAt(0);
            }

            else return null;
        }

        
        public User updateUSER(User x)
        {
            List<User> retrievedUser = USERDAL.updateUSER(x); ;
            if (retrievedUser.ElementAt(0) != null)
            {
                return retrievedUser.ElementAt(0);
            }

            else return null;
        }
    }
}