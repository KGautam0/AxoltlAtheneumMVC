using AxolotlAtheneum.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AxolotlAtheneum.DataAccessLayer
{
    public class userDAL
    {
        public bool insertUSER(User x)
        {
            //Create  SQL Connection
            SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-2K2AU8V;Initial Catalog=AxolotlAtheneum;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            //Create SQL Command with passed in stored proceduire name and SQL connection
            SqlCommand cmnd = new SqlCommand("Create_User", cnn);
            //Set Command Type
            cmnd.CommandType = CommandType.StoredProcedure;
            String cardString = JsonConvert.SerializeObject(x.card);
            String addressString = JsonConvert.SerializeObject(x.address);

            //Set Values to be passed into the stored procedure for insertion into User Table.
            cmnd.Parameters.AddWithValue("@userID", x.userID);
            cmnd.Parameters.AddWithValue("@firstName", x.firstName);
            cmnd.Parameters.AddWithValue("@lastName", x.lastName);
            cmnd.Parameters.AddWithValue("@email", x.email);
            cmnd.Parameters.AddWithValue("@Password", x.password);
            cmnd.Parameters.AddWithValue("@Status", x.status);
            cmnd.Parameters.AddWithValue("@Address", addressString);
            cmnd.Parameters.AddWithValue("@Card", cardString);
            cmnd.Parameters.AddWithValue("@Actnum", x.actnum);
            cmnd.Parameters.AddWithValue("@isAdmin", x.isAdmin);

            //Open Connection
            cnn.Open();

            //Execute Stored Procedure
            cmnd.ExecuteNonQuery();





            //Close Connection
            cnn.Close();
            return true;
        }
    }
}