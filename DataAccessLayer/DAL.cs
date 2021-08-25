using AxolotlAtheneum.Factory;
using AxolotlAtheneum.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AxolotlAtheneum.DataAccessLayer
{
    public class DAL
    {
        private String connectionstring = "server=localhost;user id=root;persistsecurityinfo=True;database=pogstore; Pwd=Kappa123!";

        public bool insertUSER(User x)
        {
            //Create  SQL Connection with Connection String
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            //Create SQL Command with passed in stored proceduire name and SQL connection object
            MySqlCommand cmnd = new MySqlCommand("Create_User", cnn);
            //Set Command Type, should  be stored procedure for this project
            cmnd.CommandType = CommandType.StoredProcedure;


            //Serialize reference type parameter 
            String cardList = JsonConvert.SerializeObject(x.cards);
            

            String addressString = JsonConvert.SerializeObject(x.address);

            //Set Values to be passed into the stored procedure for insertion into User Table.

            cmnd.Parameters.AddWithValue("p_firstName", x.firstName);
            cmnd.Parameters.AddWithValue("p_lastName", x.lastName);
            cmnd.Parameters.AddWithValue("p_email", x.email);
            cmnd.Parameters.AddWithValue("p_phonenumber", x.phonenumber);
            cmnd.Parameters.AddWithValue("p_Password", x.password);
            cmnd.Parameters.AddWithValue("p_Status", x.status);
            cmnd.Parameters.AddWithValue("p_Address", addressString);
            cmnd.Parameters.AddWithValue("p_Cards", cardList);
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
            String cardList = JsonConvert.SerializeObject(x.cards);
           
           
            

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
            cmnd.Parameters.AddWithValue("p_Cards", cardList);
            cmnd.Parameters.AddWithValue("p_Actnum", x.actnum);
            cmnd.Parameters.AddWithValue("p_isSubscribed", x.isSubscribed);
            cmnd.Parameters.AddWithValue("p_isAdmin", (x.status.Equals(Status.Admin)) ? 1 : 0);

            //Open Connection
            cnn.Open();

            //Execute Stored Procedure
            cmnd.ExecuteNonQuery();





            //Close Connection
            List<User> UserList = EMretrieveUSER(x.email, x.password);
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

                User tempuser = (User)new theFactory().factory(1);
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


                List<PaymentCard> userCard = JsonConvert.DeserializeObject<List<PaymentCard>>((string)reader["cards"]);
                tempuser.cards= userCard;
               

                userList.Add(tempuser);

            }


            //Close Connection
            cnn.Close();

            //Return User List
            return userList;
        }
        public System.Collections.Generic.List<User> IDretrieveUSER(String password, string userID)
        {
            //Create User list to store records

            List<User> userList = (List<User>)new theFactory().factory(12);

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

                User tempuser = (User)new theFactory().factory(1);
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
                List<PaymentCard> userCard = JsonConvert.DeserializeObject<List<PaymentCard>>((string)reader["cards"]);
                tempuser.cards = userCard;
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

                User tempuser = (User)new theFactory().factory(1);
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
                List<PaymentCard> userCard = JsonConvert.DeserializeObject<List<PaymentCard>>((string)reader["cards"]);
                tempuser.cards = userCard;
                userList.Add(tempuser);

            }


            //Close Connection
            cnn.Close();

            //Return User List
            return userList;
        }

        public void updatePassword(String email, String pass)
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

        public ShoppingCart addBookToCart(User user, int isbn, int quantity, double price)
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
            return getCart(user);
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
            cmnd.Parameters.AddWithValue("p_Author", x.Author);
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
            List<Book> books = (List<Book>)new theFactory().factory(11);

            while (reader.Read())
            {

                Book b = (Book)new theFactory().factory(10);
                if (!(reader["ISBN"] is DBNull))
                {
                    b.ISBN = (int)reader["ISBN"];
                }
                b.Category = (string)reader["Category"];
                b.Author = (string)reader["Author"];
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
            ShoppingCart cart = (ShoppingCart)new theFactory().factory(13);
            cart.Items = new Dictionary<Book, int>();
            Dictionary<int, int> items = new Dictionary<int, int>(); //isbn, int

            while (reader.Read())
            {
                if (!(reader["Book_ISBN"] is DBNull) && !(reader["Book_Quantity"] is DBNull))
                {
                    if (!items.ContainsKey((int)reader["Book_ISBN"]))
                        items.Add((int)reader["Book_ISBN"], (int)reader["Book_Quantity"]);
                    else
                        items[(int)reader["Book_ISBN"]] += (int)reader["Book_Quantity"];
                }

            }
            cnn.Close();

            cart.UserID = user.userID;
            foreach (var item in items)
            {
                //find book based on isbn
                Book book = filterBooks(item.Key.ToString(), QueryCategory.ISBN.ToString())[0];
                if (book != null)
                {
                    //add book and its quantity to cart, and change cart total $

                    cart.Items.Add(book, item.Value);
                    cart.Total += (book.SellingPrice * item.Value);
                }
            }

            return cart;
        }

        public List<Book> filterBooks(string query, string searchCommand)
        {
            string procedure = "searchBook" + searchCommand;
            string parameter = "p_" + searchCommand;
            if (searchCommand.Equals("Author"))
                parameter += "_Name";
            List<Book> results = new List<Book>();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand(procedure, cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue(parameter, query);

            cnn.Open();
            var reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                Book b = (Book)new theFactory().factory(10);
                if (!(reader["ISBN"] is DBNull))
                {
                    b.ISBN = (int)reader["ISBN"];
                    b.Category = (string)reader["Category"];
                    b.Author = (string)reader["Author"];
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

            }
            cnn.Close();
            return results;
        }



        public List<Book> getCartBooks(User x)
        {

            List<Book> bookList = (List<Book>)new theFactory().factory(11);
            List<String> isbnList = new List<String>();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand("get_cartBooks", cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("p_userID", x.userID);

            cnn.Open();
            var reader = cmnd.ExecuteReader();
            while (reader.Read())
            {

                if (!(reader["ISBN"] is DBNull))
                {
                    String isbn = ((int)reader["Book_ISBN"]).ToString();
                    int quant = ((int)reader["Book_Quantity"]);

                    for (int y = 0; y < quant; y++)
                    {
                        isbnList.Add(isbn);
                    }


                }

            }
            reader.Close();
            cnn.Close();

            cmnd = new MySqlCommand("get_books", cnn);

            cmnd.Parameters.AddWithValue("p_userID", x.userID);
            cnn.Open();
            foreach (String u in isbnList)
            {
                cmnd.Parameters.AddWithValue("p_ISBN", u);
                reader = cmnd.ExecuteReader();
                Book b = (Book)new theFactory().factory(10);
                if (!(reader["ISBN"] is DBNull))
                {
                    b.ISBN = (int)reader["ISBN"];
                    b.Category = (string)reader["Category"];
                    b.Author = (string)reader["Author"];
                    b.Title = (string)reader["Title"];
                    b.CoverPictureURL = (string)reader["Cover_Picture"];
                    b.Edition = (int)reader["Edition"];
                    b.Publisher = (string)reader["Publisher"];
                    b.PublicationYear = (int)reader["Publication_Year"];
                    b.QuantityInStock = (int)reader["Quantity"];
                    b.MinimumThreshold = (int)reader["Minimum_Thresh"];
                    b.BuyingPrice = (double)reader["Buying_Price"];
                    b.SellingPrice = (double)reader["Selling_Price"];

                    bookList.Add(b);
                }

            }
            cnn.Close();
            return bookList;
        }

        public ShoppingCart removeFromCart(User user, Book book)
        {
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand("deleteAllBookSC", cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("p_UserID", user.userID);
            cmnd.Parameters.AddWithValue("p_BookISBN", book.ISBN);

            cnn.Open();
            cmnd.ExecuteNonQuery();
            cnn.Close();

            return getCart(user);
        }


        public List<User> getSubscribedUsers()
        {
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand("returnSubbedUsers", cnn);
            cmnd.CommandType = CommandType.StoredProcedure;

            cnn.Open();
            List<User> users = new List<User>();
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
                if (!(reader["isSubscribed"] is DBNull))
                    tempuser.isSubscribed = Convert.ToInt32(reader["isSubscribed"]);
                else
                    tempuser.isSubscribed = 0;

                //optional values
                string addressJson = reader["address"] as string;
                if (addressJson != null)
                {
                    Address userAddress = JsonConvert.DeserializeObject<Address>(addressJson);
                    tempuser.address = userAddress;
                }
                tempuser.cards = new List<PaymentCard>();
                string cardJson = reader["card0"] as string;
                if (cardJson != "" && cardJson != null)
                {
                    PaymentCard userCard = JsonConvert.DeserializeObject<PaymentCard>(cardJson);
                    tempuser.cards.Add(userCard);
                    tempuser.cards[0].index = 0;
                }
                cardJson = reader["card1"] as string;
                if (cardJson != "" && cardJson != null)
                {
                    PaymentCard userCard = JsonConvert.DeserializeObject<PaymentCard>(cardJson);
                    tempuser.cards.Add(userCard);
                    tempuser.cards[1].index = 1;
                }
                cardJson = reader["card2"] as string;
                if (cardJson != "" && cardJson != null)
                {
                    PaymentCard userCard = JsonConvert.DeserializeObject<PaymentCard>(cardJson);
                    tempuser.cards.Add(userCard);
                    tempuser.cards[2].index = 2;
                }

                users.Add(tempuser);

            }

            return users;
        }

        public Promotion getPromo(string promoCode)
        {
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand("getPromotion", cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("p_promotionCode", promoCode);

            cnn.Open();
            List<Promotion> promos = new List<Promotion>();
            var reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                Promotion promo = new Promotion();
                if (!(reader["Promotion_Code"] is DBNull))
                {
                    promo.PromoCode = (string)reader["Promotion_Code"];
                }
                if (!(reader["Promotion_Name"] is DBNull))
                {
                    promo.PromoName = (string)reader["Promotion_Name"];
                }
                promo.StartDate = (DateTime)reader["Promotion_Start"];
                promo.EndDate = (DateTime)reader["Promotion_End"];
                promo.ValueOff = (double)reader["Promotion_Discount"];
            }
            cnn.Close();

            if (promos.Count != 0)
                return promos[0];
            else
                return null;
        }

        public void addToOrder(Order x)
        {
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand("addToOrders", cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("p_OrderID", x.OrderID);
            cmnd.Parameters.AddWithValue("p_CustomerID", x.orderuserID);
            cmnd.Parameters.AddWithValue("p_OrderPrice", x.price);
            cmnd.Parameters.AddWithValue("p_OrderAdress", JsonConvert.SerializeObject(x.ShippingAddress));
            cmnd.Parameters.AddWithValue("p_OrderPayment", JsonConvert.SerializeObject(x.PaymentMethod));
            cmnd.Parameters.AddWithValue("p_OrderISBN", JsonConvert.SerializeObject(x.Items));
            
            cmnd.Parameters.AddWithValue("p_OrderStatus", x.OrderStatus);
            cnn.Open();
            cmnd.ExecuteNonQuery();
            
            cnn.Close();

            
        }
       
        public void addPromo(Promotion x)
        {
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand("addPromotion", cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("p_PromoID", x.PromoName);
            cmnd.Parameters.AddWithValue("p_PromoCode", x.PromoCode);
            cmnd.Parameters.AddWithValue("p_PromoStart", x.StartDate);
            cmnd.Parameters.AddWithValue("p_PromoEnd", x.EndDate);
            cmnd.Parameters.AddWithValue("p_PromoDiscount", x.ValueOff);
 
            cnn.Open();
            cmnd.ExecuteNonQuery();

            cnn.Close();


        }
      

        public List<Order> getOrders(User x)
        {
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            MySqlCommand cmnd = new MySqlCommand("getOrders", cnn);
            cmnd.CommandType = CommandType.StoredProcedure;
            cmnd.Parameters.AddWithValue("p_UserIDe", x.userID);

            cnn.Open();
            List<Order> orderList = new List<Order>();
            var reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                Order order = (Order)new theFactory().factory(14);
                
                order.OrderID = (int)reader["Order_ID"];
                order.orderuserID = (int)reader["User_ID"];
                order.DateOrdered = (DateTime)reader["Order_Time"];
                order.price = (int)reader["Order_Price"];

                order.OrderStatus = (OrderStatus)reader["Order_Status"];

                order.ShippingAddress = JsonConvert.DeserializeObject<Address>((String)reader["Order_Address"]);
                order.PaymentMethod = JsonConvert.DeserializeObject<PaymentCard>((String)reader["Order_Payment"]);
                order.Items = JsonConvert.DeserializeObject<Dictionary<Book, int>>((String)reader["Order_Payment"]);

                orderList.Add(order);
            }
            cnn.Close();

           
                return orderList;


        }

    }
}