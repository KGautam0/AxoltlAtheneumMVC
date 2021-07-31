CREATE PROCEDURE editBook @old_ISBN INT, @ISBN INT, @Category varchar(45), @Author_Name varchar(45), @Title varchar(45), @Cover_Picture varchar(45), @Edition INT, @Publisher varchar(45), @Publication_Year INT, @Quantity INT, @MinThresh INT, @BuyPrice INT, @SellPrice INT 
AS
DELETE  * FROM books where ISBN = @old_ISBN
INSERT INTO books VALUES(@ISBN, @Category, @Author_Name, @Title, @Cover_Picture, @Edition, @Publisher, @Publication_Year, @Quantity, @MinThresh, @BuyPrice, @SellPrice)
GO;