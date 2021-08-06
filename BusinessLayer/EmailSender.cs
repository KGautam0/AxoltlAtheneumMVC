using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.IO;
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
        public void sendEditNotice(User x, int format)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("axolotl.atheneum@gmail.com");
            mail.To.Add(x.email);
            mail.Subject = "Your Account Info has been updated";
            switch (format)
                {
                case 1: mail.Body = "This is a notice that your name linked to your Axolotl Atheneum Account has been changed.";
                    break;
                case 2:
                    mail.Body = "This is a notice that your phone number linked to your Axolotl Atheneum Account has been changed.";
                    break;
                case 3:
                    mail.Body = "This is a notice that your address linked to your Axolotl Atheneum Account has been changed.";
                    break;
                case 4:
                    mail.Body = "This is a notice that your Credit Card linked to your Axolotl Atheneum Account has been changed.";
                    break;
            }
            

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

        public void sendPromo(string email, string discountCode, string discount, DateTime start, DateTime end)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("axolotl.atheneum@gmail.com");
            mail.To.Add(email);
            mail.Subject = "New Promotional Discount from Axolotl Atheneum";
            mail.Body = "Cool new promotion! Enter the discount code: " + discountCode + " for a discount of " + discount + " starting on " + start + " and ending on " + end;

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("axolotl.atheneum", "MidTermCurve");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }

        public void sendCartConfirmation(User x)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("axolotl.atheneum@gmail.com");
            mail.To.Add(x.email);
            mail.Subject = "Order Confirmation from Axolotl Atheneum";
            mail.Body = "Thank you for Shopping at Axolotl Atheneum. " +
                "Check your Order History for more information!";
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("axolotl.atheneum", "MidTermCurve");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

    }
}