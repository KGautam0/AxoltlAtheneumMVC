CREATE PROCEDURE createCart ( p_UserID INT)
BEGIN
INSERT INTO shopping_cart(Customer_ID) VALUE(@UserID)
END;
