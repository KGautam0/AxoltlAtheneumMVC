CREATE PROCEDURE addToOrders @OrderID INT, @CustomerID VARCHAR(45), @OrderTime TIME, @OrderStatus VARCHAR(45), @OrderPrice INT, @OrderAdress VARCHAR(255), @OrderPayment VARCHAR(45), @OrderISBN VARCHAR(45)
AS
INSERT INTO orders VALUES(@OrderID, @CustomerID, @OrderTime, @OrderStatus, @OrderPrice, @OrderAdress, @OrderPayment, @OrderISBN)
GO;