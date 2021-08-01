CREATE PROCEDURE searchBookCategory ( p_Category varchar(45))
BEGIN
SELECT * FROM books WHERE Category = p_Category;
END;
