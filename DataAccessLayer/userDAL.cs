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
        private String connectionstring = "server=localhost;user id=root;database=bookstore;pwd=Twix!1721mnj";

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

        public void addBookToCart(User user, int isbn, int quantity, double price)
        {
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            MySqlCommand cmnd = new MySqlCommand("addBookToCart", cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("p_UserID", user.userID);
            cmnd.Parameters.AddWithValue("p_ISBN", isbn);
            cmnd.Parameters.AddWithValue("p_Quantity", quantity);
            cmnd.Parameters.AddWithValue("p_Price", price);

            cnn.Open();
            cmnd.ExecuteNonQuery();
            cnn.Close();
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

        public List<Book> getAllBooks()
        {
            //Create  SQL Connection with Connection String
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection object
            MySqlCommand cmnd = new MySqlCommand("returnAllBooks", cnn);
            //Set Command Type, should  be stored procedure for this project
            cmnd.CommandType = CommandType.StoredProcedure;

            cnn.Open();

            //Execute Reader
            var reader = cmnd.ExecuteReader();
            List<Book> books = new List<Book>();

            while (reader.Read())
            {

                Book b = new Book();
                if (!(reader["ISBN"] is DBNull))
                {
                    b.ISBN = (int)reader["ISBN"];
                }
                b.Category = (string)reader["Category"];
                b.Author = (string)reader["Author_Name"];
                b.Title = (string)reader["Title"];
                b.CoverPictureURL = (string)reader["Cover_Picture"];
                b.Edition = (int)reader["Edition"];
                b.Publisher = (string)reader["Publisher"];
                b.PublicationYear = (int)reader["Publication_Year"];
                b.QuantityInStock = (int)reader["Quantity"];
                b.MinimumThreshold = (int)reader["Minimum_Thresh"];
                b.BuyingPrice = (double)reader["Buying_Price"];
                b.SellingPrice = (double)reader["Selling_Price"];

                books.Add(b);

            }

            cnn.Close();

            return books;
        }

        public ShoppingCart getCart(User user)
        {
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand("viewCustomerCart", cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("p_UserID", user.userID);

            cnn.Open();

            //Execute Reader
            var reader = cmnd.ExecuteReader();
            ShoppingCart cart = new ShoppingCart();
            Dictionary<int, int> items = new Dictionary<int, int>(); //isbn, int

            while (reader.Read())
            {
                
                if (!(reader["User_ID"] is DBNull))
                    cart.UserID = (int)reader["User_ID"];

                if (!items.ContainsKey((int)reader["ISBN"]))
                    items.Add((int)reader["isbn"], (int)reader["quantity"]);
                else
                    items[(int)reader["isbn"]] += (int)reader["quantity"];

            }
            cnn.Close();

            foreach(var item in items)
            {
                //find book based on isbn
                Book book = filterBooks(item.Key.ToString(), QueryCategory.Author.ToString())[0];
                if (book != null)
                {
                    //add book and its quantity to cart, and change cart total $
                    cart.Items.Add(book, item.Value);
                    cart.Total += book.SellingPrice;
                }
            }

            return cart;
        }

        public List<Book> filterBooks(string title, string searchCommand)
        {
            string proc = "searchBook" + searchCommand;
            List<Book> results = new List<Book>();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand(proc, cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("p_Title", title);

            cnn.Open();
            var reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                Book b = new Book();
                if (!(reader["ISBN"] is DBNull))
                {
                    b.ISBN = (int)reader["ISBN"];
                }
                b.Category = (string)reader["Category"];
                b.Author = (string)reader["Author_Name"];
                b.Title = (string)reader["Title"];
                b.CoverPictureURL = (string)reader["Cover_Picture"];
                b.Edition = (int)reader["Edition"];
                b.Publisher = (string)reader["Publisher"];
                b.PublicationYear = (int)reader["Publication_Year"];
                b.QuantityInStock = (int)reader["Quantity"];
                b.MinimumThreshold = (int)reader["Minimum_Thresh"];
                b.BuyingPrice = (double)reader["Buying_Price"];
                b.SellingPrice = (double)reader["Selling_Price"];

                results.Add(b);

            }
            cnn.Close();
            return results;
        }


    }
}