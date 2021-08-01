CREATE PROCEDURE viewCustomerCart ( p_UserID INT)
BEGIN
SELECT * FROM shopping_carts WHERE Customer_ID = p_UserID;
END;
