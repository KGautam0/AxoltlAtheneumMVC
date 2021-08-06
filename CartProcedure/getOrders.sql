CREATE PROCEDURE getCustomerOrders ( p_customerID INT)
BEGIN
SELECT * FROM orders WHERE Customer_ID = p_customerID;
END;
