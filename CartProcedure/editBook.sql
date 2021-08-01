CREATE PROCEDURE editBook ( p_old_ISBN INT, p_ISBN INT, p_Category varchar(45), p_Author_Name varchar(45), p_Title varchar(45), p_Cover_Picture varchar(45), p_Edition INT, p_Publisher varchar(45), p_Publication_Year INT, p_Quantity INT, p_MinThresh INT, p_BuyPrice INT, p_SellPrice INT) 
BEGIN
DELETE * FROM books where ISBN = p_old_ISBN
INSERT INTO books(ISBN, Category, Author_Name, Title, Cover_Pictrue, Edition, Publisher, Publication_Year, Quantity, Minimum_thresh, Buying_Price, Selling_Price) 
VALUES(p_ISBN, p_Category, p_Author_Name, p_Title, p_Cover_Picture, p_Edition, p_Publisher, p_Publication_Year, p_Quantity, p_MinThresh, p_BuyPrice, p_SellPrice);
END;
