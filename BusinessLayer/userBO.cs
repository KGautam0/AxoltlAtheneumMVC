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
        userDAL USERDAL = new userDAL();
        public void regUSER(User x)
        {
           
            if (USERDAL.insertUSER(x))
            {
                EmailSender messenger = new EmailSender();
                messenger.sendACTNUM(x);
            }
        }

        public void verUSER(User x)
        {

            
                EmailSender messenger = new EmailSender();
                messenger.sendUserID(x);
            
        }


        public void resetPass(String email, int actnum)
        {


            EmailSender messenger = new EmailSender();
            messenger.resetPass(email, actnum);

        }


        public User EMlogUSER(String email, String password)
        {
            List<User> retrievedUser = USERDAL.EMretrieveUSER(email, password);
            if (retrievedUser.Count() != 0)
            {
                return retrievedUser.ElementAt(0);
            }

            else return null;
        }
        public User IDlogUSER(String password, String userID)
        {
            List<User> retrievedUser = USERDAL.IDretrieveUSER(userID, password);
            if (retrievedUser.Count() != 0)
            {
                return retrievedUser.ElementAt(0);
            }

            else return null;
        }

        public bool checkUSER(String email)
        {
            List<User> retrievedUser = USERDAL.checkUSER(email);
            if (retrievedUser.Count!=0)
            {
                return true;
            }

            else return false;
        }

        public void updatePass(String email, String pass)
        {
            USERDAL.updatePassword(email, pass);
        }

        public bool confirmPass(String email, String pass)
        {
            return USERDAL.EMretrieveUSER(email, pass).Count != 0;
        }

        public void sendEditNotice(User x, int format)
        {
            EmailSender messenger = new EmailSender();
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

        public User getUser(String email)
        {
            List<User> users = USERDAL.checkUSER(email);
            User user = null;
            if (users != null)
            {
                user = users[0];
            }
            return user;
        }
    }
}