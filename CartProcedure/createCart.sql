CREATE PROCEDURE createCart @UserID INT
AS
INSERT INTO shopping_cart VALUE(@UserID)
GO;