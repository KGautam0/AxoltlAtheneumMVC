CREATE PROCEDURE deleteAllBookSC ( p_BookISBN INT, p_UserID INT)
BEGIN
DELETE * FROM shopping_carts WHERE Book_ISBN = p_BookISBN AND Customer_ID = p_UserID;
END;
