CREATE PROCEDURE addBookToCart ( p_USERID INT, p_ISBN INT, p_PromoCode VARCHAR(45), p_Quantity INT, p_Price INT)
BEGIN
INSERT INTO shopping_carts(Customer_ID, Book_ISBN, Promo_Code, Cart_Total) VALUES(p_USERID, p_ISBN, p_PromoCode, p_Quantity * p_Price);
END;
