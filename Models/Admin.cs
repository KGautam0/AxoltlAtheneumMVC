using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.Models
{
    public class Admin : User
    {
        public void changeUserStatus(User x, Status newRank)
        {
            if (this.status.Equals(Status.Admin))
            {
                x.status = newRank;
            }
        }
        public void PromoteUser(User User)
        {
            changeUserStatus(User, Status.Admin);
        }
        public void DemoteUser(User User)
        {
            changeUserStatus(User, Status.Active);
        }
        public void SuspendUser(User User)
        {
            changeUserStatus(User, Status.Suspended);
        }
        public void AddBook(User Book)
        {

        }
        public void DeleteBook(User Book)
        {

        }
        public void EditBook(Book Book, String Property, String Value)
        {

        }
        public void AddPromo(String PromoCode, String PromoName, float ValueOff, DateTime StartDate, DateTime EndDate)
        {

        }
    }
}   