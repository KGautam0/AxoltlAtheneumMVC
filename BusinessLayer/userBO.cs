using AxolotlAtheneum.DataAccessLayer;
using AxolotlAtheneum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.BusinessLayer
{
    public class userBO
    {

        public void regUSER(User x)
        {
            userDAL USERDAL = new userDAL();
            if (USERDAL.insertUSER(x))
            {
                EmailSender messenger = new EmailSender();
                messenger.sendACTNUM(x);
            }
        }

    }
}