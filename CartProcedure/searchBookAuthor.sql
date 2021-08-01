CREATE PROCEDURE searchBookAuthor ( p_Author varchar(45))
BEGIN
SELECT * FROM books WHERE Author_Name = p_Author;
END;
