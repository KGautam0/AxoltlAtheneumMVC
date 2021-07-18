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
    }
}