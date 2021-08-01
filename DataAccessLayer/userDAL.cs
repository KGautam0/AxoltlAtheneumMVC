﻿using AxolotlAtheneum.Models;
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
        private String connectionstring = "server=localhost;user id=root; Pwd=2020CoffeeXD!; database=axolotlatheneum";

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
            cmnd.Parameters.AddWithValue("p_isAdmin", (x.status.Equals(Status.Admin)) ? 1 : 0);

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
            cmnd.Parameters.AddWithValue("p_userID", x.userID);
            cmnd.Parameters.AddWithValue("p_lastName", x.lastName);
            cmnd.Parameters.AddWithValue("p_email", x.email);
            cmnd.Parameters.AddWithValue("p_Password", x.password);
            cmnd.Parameters.AddWithValue("p_Phonenumber", x.phonenumber);
            cmnd.Parameters.AddWithValue("p_Status", x.status);
            cmnd.Parameters.AddWithValue("p_Address", addressString);
            cmnd.Parameters.AddWithValue("p_Card", cardString);
            cmnd.Parameters.AddWithValue("p_Actnum", x.actnum);
            cmnd.Parameters.AddWithValue("p_isAdmin", (x.status.Equals(Status.Admin)) ? 1 : 0);

            //Open Connection
            cnn.Open();

            //Execute Stored Procedure
            cmnd.ExecuteNonQuery();





            //Close Connection
            List<User> UserList= EMretrieveUSER(x.email, x.password);
            cnn.Close();
            return UserList;
        }

        public System.Collections.Generic.List<User> EMretrieveUSER(String email, String password)
        {
            //Create User list to store records

            List<User> userList = new List<User>();

            //Create  SQL Connection
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection
            MySqlCommand cmnd = new MySqlCommand("EMretrieve_user", cnn);
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
                tempuser.status = (Status)((int)reader["status"]);
                tempuser.actnum = Convert.ToInt32(reader["actnum"]);

                //optional values
                string addressJson = reader["address"] as string;
                if (addressJson != null)
                {
                    Address userAddress = JsonConvert.DeserializeObject<Address>(addressJson);
                    tempuser.address = userAddress;
                }
                string cardJson = reader["card"] as string;
                if (cardJson != null)
                {
                    PaymentCard userCard = JsonConvert.DeserializeObject<PaymentCard>(cardJson);
                    tempuser.card = userCard;
                }
                
                userList.Add(tempuser);

            }


            //Close Connection
            cnn.Close();

            //Return User List
            return userList;
        }
        public System.Collections.Generic.List<User> IDretrieveUSER( String password, string userID)
        {
            //Create User list to store records

            List<User> userList = new List<User>();

            //Create  SQL Connection
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection
            MySqlCommand cmnd = new MySqlCommand("IDretrieve_user", cnn);
            //Set Command Type
            cmnd.CommandType = CommandType.StoredProcedure;

            //Set Values to be passed into the stored procedure for insertion into User Table.
            
            cmnd.Parameters.AddWithValue("p_Password", password);
            cmnd.Parameters.AddWithValue("p_userID", userID);
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
                tempuser.status = (Status)((int)reader["status"]);
                tempuser.actnum = Convert.ToInt32(reader["actnum"]);
                //optional values
                string addressJson = reader["address"] as string;
                if (addressJson != null)
                {
                    Address userAddress = JsonConvert.DeserializeObject<Address>(addressJson);
                    tempuser.address = userAddress;
                }
                string cardJson = reader["card"] as string;
                if (cardJson != null)
                {
                    PaymentCard userCard = JsonConvert.DeserializeObject<PaymentCard>(cardJson);
                    tempuser.card = userCard;
                }
                userList.Add(tempuser);

            }


            //Close Connection
            cnn.Close();

            //Return User List
            return userList;
        }

        public System.Collections.Generic.List<User> checkUSER(String email)
        {
            //Create User list to store records

            List<User> userList = new List<User>();

            //Create  SQL Connection
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection
            MySqlCommand cmnd = new MySqlCommand("check_user", cnn);
            //Set Command Type
            cmnd.CommandType = CommandType.StoredProcedure;

            //Set Values to be passed into the stored procedure for insertion into User Table.
            cmnd.Parameters.AddWithValue("p_Email", email);

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
                tempuser.status = (Status)((int)reader["status"]);
                tempuser.actnum = Convert.ToInt32(reader["actnum"]);
                //optional values
                string addressJson = reader["address"] as string;
                if (addressJson != null)
                {
                    Address userAddress = JsonConvert.DeserializeObject<Address>(addressJson);
                    tempuser.address = userAddress;
                }
                string cardJson = reader["card"] as string;
                if (cardJson != null)
                {
                    PaymentCard userCard = JsonConvert.DeserializeObject<PaymentCard>(cardJson);
                    tempuser.card = userCard;
                }
                userList.Add(tempuser);

            }


            //Close Connection
            cnn.Close();

            //Return User List
            return userList;
        }

        public void updatePassword(String email, String pass )
        {
            //Create  SQL Connection with Connection String
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection object
            MySqlCommand cmnd = new MySqlCommand("reset_pass", cnn);
            //Set Command Type, should  be stored procedure for this project
            cmnd.CommandType = CommandType.StoredProcedure;

            //Serialize reference type parameter 
            

            //Set Values to be passed into the stored procedure for insertion into User Table.

           
            cmnd.Parameters.AddWithValue("p_email", email);
           
            cmnd.Parameters.AddWithValue("p_Password", pass);
            

            //Open Connection
            cnn.Open();

            //Execute Stored Procedure
            cmnd.ExecuteNonQuery();





            //Close Connection
            cnn.Close();
            
        }

        public List<Book> getBooks()
        {
            //Create  SQL Connection with Connection String
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //get all the books for no search params
            List<Book> books = new List<Book>();
            return books;
        }

        public List<Book> getBooks(String query, String category)
        {
            //Create  SQL Connection with Connection String
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //get all the books for no search params
            List<Book> books = new List<Book>();
            return books;
        }

        public void addBookToCart(User user, int isbn, string promo, int quantity, int price)
        {
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            
        }

        public void insertBook(Book x)
        {
            //Create  SQL Connection with Connection String
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection object
            MySqlCommand cmnd = new MySqlCommand("addBook", cnn);
            //Set Command Type, should  be stored procedure for this project
            cmnd.CommandType = CommandType.StoredProcedure;

            //Set Values to be passed into the stored procedure for insertion into Book Table.
            cmnd.Parameters.AddWithValue("p_ISBN", x.ISBN);
            cmnd.Parameters.AddWithValue("p_Category", x.Category);
            cmnd.Parameters.AddWithValue("p_Author_Name", x.Author);
            cmnd.Parameters.AddWithValue("p_Title", x.Title);
            cmnd.Parameters.AddWithValue("p_Cover_Picture", x.CoverPictureURL);
            cmnd.Parameters.AddWithValue("p_Edition", x.Edition);
            cmnd.Parameters.AddWithValue("p_Publisher", x.Publisher);
            cmnd.Parameters.AddWithValue("p_Publication_Year", x.PublicationYear);
            cmnd.Parameters.AddWithValue("p_Quantity", x.QuantityInStock);
            cmnd.Parameters.AddWithValue("p_MinThresh", x.MinimumThreshold);
            cmnd.Parameters.AddWithValue("p_BuyPrice", x.BuyingPrice);
            cmnd.Parameters.AddWithValue("p_SellPrice", x.SellingPrice);

            //Open Connection
            cnn.Open();

            //Execute Stored Procedure
            cmnd.ExecuteNonQuery();

            //Close Connection
            cnn.Close();
        }


    }
}