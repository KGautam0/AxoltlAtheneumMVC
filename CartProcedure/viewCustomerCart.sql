CREATE PROCEDURE viewCustomerCart @UserID INT
AS
SELECT * FROM shopping_carts WHERE Customer_ID = @USERID
GO;