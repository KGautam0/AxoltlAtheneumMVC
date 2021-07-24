using AxolotlAtheneum.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AxolotlAtheneum.DataAccessLayer
{
    public class userDAL
    {
        public bool insertUSER(User x)
        {
            //Create  SQL Connection with Connection String
            SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-2K2AU8V;Initial Catalog=AxolotlAtheneum;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            //Create SQL Command with passed in stored proceduire name and SQL connection object
            SqlCommand cmnd = new SqlCommand("Create_User", cnn);
            //Set Command Type, should  be stored procedure for this project
            cmnd.CommandType = CommandType.StoredProcedure;

            //Serialize reference type parameter 
            String cardString = JsonConvert.SerializeObject(x.card);
            String addressString = JsonConvert.SerializeObject(x.address);

            //Set Values to be passed into the stored procedure for insertion into User Table.
            
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


        public System.Collections.Generic.List<User> updateUSER(User x)
        {
            //Create  SQL Connection with Connection String
            SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-2K2AU8V;Initial Catalog=AxolotlAtheneum;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            //Create SQL Command with passed in stored proceduire name and SQL connection object
            SqlCommand cmnd = new SqlCommand("update_user", cnn);
            //Set Command Type, should  be stored procedure for this project
            cmnd.CommandType = CommandType.StoredProcedure;

            //Serialize reference type parameter 
            String cardString = JsonConvert.SerializeObject(x.card);
            String addressString = JsonConvert.SerializeObject(x.address);

            //Set Values to be passed into the stored procedure for insertion into User Table.

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
            return retrieveUSER(x.email, x.password);
        }

        public System.Collections.Generic.List<User> retrieveUSER(String email, String password)
        {
            //Create User list to store records

            List<User> userList = new List<User>();

            //Create  SQL Connection
            SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-2K2AU8V;Initial Catalog=AxolotlAtheneum;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            //Create SQL Command with passed in stored proceduire name and SQL connection
            SqlCommand cmnd = new SqlCommand("retrieve_user", cnn);
            //Set Command Type
            cmnd.CommandType = CommandType.StoredProcedure;

            //Set Values to be passed into the stored procedure for insertion into User Table.
            cmnd.Parameters.AddWithValue("@Email", email);
            cmnd.Parameters.AddWithValue("@Password", password);

            //Open Connection
            cnn.Open();

            //Execute Reader
            var reader = cmnd.ExecuteReader();

            while (reader.Read())
            {

                User tempuser = new User();
                tempuser.userID = (int)reader["userID"];
                tempuser.firstName = (string)reader["firstname"];
                tempuser.lastName = (string)reader["lastname"];
                tempuser.email = (string)reader["email"];
                tempuser.password = (string)reader["password"];
                tempuser.status = (int)reader["status"];
                tempuser.actnum = Convert.ToInt32(reader["actnum"]);
                string addressJson = (string)reader["address"];
                Address userAddress = JsonConvert.DeserializeObject<Address>(addressJson);
                tempuser.address = userAddress;
                string cardJson = (string)reader["card"];
                PaymentCard userCard = JsonConvert.DeserializeObject<PaymentCard>(cardJson);
                tempuser.isAdmin = Convert.ToBoolean(reader["isAdmin"]);
                userList.Add(tempuser);

            }


            //Close Connection
            cnn.Close();

            //Return User List
            return userList;
        }
    }
}