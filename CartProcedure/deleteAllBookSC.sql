CREATE PROCEDURE deleteAllBookSC ( p_BookISBN INT)
BEGIN
DELETE * FROM shopping_carts WHERE Book_ISBN = p_BookISBN;
END;
