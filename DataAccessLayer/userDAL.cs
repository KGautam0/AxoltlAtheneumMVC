using AxolotlAtheneum.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AxolotlAtheneum.DataAccessLayer
{
    public class userDAL
    {
        private String connectionstring = "server=localhost;user id=TesterUser;Pwd=Test123!;database=axolotlatheneum;persistsecurityinfo=True";

        public bool insertUSER(User x)
        {
            //Create  SQL Connection with Connection String
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection object
            MySqlCommand cmnd = new MySqlCommand("Create_User", cnn);
            //Set Command Type, should  be stored procedure for this project
            cmnd.CommandType = CommandType.StoredProcedure;

            //Serialize reference type parameter 
            String cardString = JsonConvert.SerializeObject(x.card);
            String addressString = JsonConvert.SerializeObject(x.address);

            //Set Values to be passed into the stored procedure for insertion into User Table.
            
            cmnd.Parameters.AddWithValue("p_firstName", x.firstName);
            cmnd.Parameters.AddWithValue("p_lastName", x.lastName);
            cmnd.Parameters.AddWithValue("p_email", x.email);
            cmnd.Parameters.AddWithValue("p_phonenumber", x.phonenumber);
            cmnd.Parameters.AddWithValue("p_Password", x.password);
            cmnd.Parameters.AddWithValue("p_Status", x.status);
            cmnd.Parameters.AddWithValue("p_Address", addressString);
            cmnd.Parameters.AddWithValue("p_Card", cardString);
            cmnd.Parameters.AddWithValue("p_Actnum", x.actnum);
            cmnd.Parameters.AddWithValue("p_isAdmin", x.isAdmin);

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
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection object
            MySqlCommand cmnd = new MySqlCommand("update_user", cnn);
            //Set Command Type, should  be stored procedure for this project
            cmnd.CommandType = CommandType.StoredProcedure;

            //Serialize reference type parameter 
            String cardString = JsonConvert.SerializeObject(x.card);
            String addressString = JsonConvert.SerializeObject(x.address);

            //Set Values to be passed into the stored procedure for insertion into User Table.

            cmnd.Parameters.AddWithValue("p_firstName", x.firstName);
            cmnd.Parameters.AddWithValue("p_lastName", x.lastName);
            cmnd.Parameters.AddWithValue("p_email", x.email);
            cmnd.Parameters.AddWithValue("p_Password", x.password);
            cmnd.Parameters.AddWithValue("p_Status", x.status);
            cmnd.Parameters.AddWithValue("p_Address", addressString);
            cmnd.Parameters.AddWithValue("p_Card", cardString);
            cmnd.Parameters.AddWithValue("p_Actnum", x.actnum);
            cmnd.Parameters.AddWithValue("p_isAdmin", x.isAdmin);

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
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection
            MySqlCommand cmnd = new MySqlCommand("retrieve_user", cnn);
            //Set Command Type
            cmnd.CommandType = CommandType.StoredProcedure;

            //Set Values to be passed into the stored procedure for insertion into User Table.
            cmnd.Parameters.AddWithValue("p_Email", email);
            cmnd.Parameters.AddWithValue("p_Password", password);

            //Open Connection
            cnn.Open();

            //Execute Reader
            var reader = cmnd.ExecuteReader();

            while (reader.Read())
            {

                User tempuser = new User();
                if (!(reader["userID"] is DBNull))
                {
                    tempuser.userID = (string)reader["userID"];
                }
                tempuser.phonenumber = (String)reader["phonenumber"];
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