CREATE PROCEDURE addToOrders ( p_OrderID INT, p_CustomerID VARCHAR(45), p_OrderTime TIME(6), p_OrderStatus VARCHAR(45), p_OrderPrice INT, p_OrderAdress VARCHAR(255), p_OrderPayment VARCHAR(45), p_OrderISBN VARCHAR(45))
BEGIN
INSERT INTO orders(Order_ID, Customer_ID, Order_Time, Order_Status, Order_Price, Order_Address, Order_Payment, Ordered_ISBN) 
VALUES(p_OrderID, p_CustomerID, p_OrderTime, p_OrderStatus, p_OrderPrice, p_OrderAdress, p_OrderPayment, p_OrderISBN);
END;
