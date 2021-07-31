CREATE PROCEDURE addBookToCart @USERID INT, @ISBN INT, @PromoCode VARCHAR(45), @Quantity INT, @Price INT
AS
INSERT INTO shopping_carts VALUES(@USERID, @ISBN, @PromoCode, @Quantity * @Price)
GO;