CREATE PROCEDURE deleteRepeatBook( p_UserID INT, p_BookISBN)
BEGIN
DELETE FROM shopping_carts WHERE Customer_ID = p_UserID AND Book_ISBN = p_BookISBN NOT IN (SELECT * FROM (SELECT MIN(p_UserID) GROUP BY p_BookISBN, p_UserID) AS repeatedBook);
END;