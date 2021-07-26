using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Net.Mail;
using System.Web;
using Dnp.Net;
using AxolotlAtheneum.Models;

namespace AxolotlAtheneum.BusinessLayer
{
    public class EmailSender
    {
        public void sendACTNUM(User x)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("axolotl.atheneum@gmail.com");
            mail.To.Add(x.email);
            mail.Subject = "Account Verification";
            mail.Body = "Your account has been created but is inactive as it hasn't been verified, your priveleges are greately limited. Verify your account with this number " + x.actnum; 

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("axolotl.atheneum", "MidTermCurve");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }

        public void sendUserID(User x)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("axolotl.atheneum@gmail.com");
            mail.To.Add(x.email);
            mail.Subject = "Account Verification";
            mail.Body = "Your account has been verified.You have now been assigned a userID that can be used to sign in instead of your email. UserID: " + x.userID;

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("axolotl.atheneum", "MidTermCurve");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }

        public void resetPass(String email, int actnum)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("axolotl.atheneum@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Password Reset";
            mail.Body = "You requeted a password change, enter this number along with your new password on the change password page to change your password." + actnum;

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("axolotl.atheneum", "MidTermCurve");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }

    }
}