CREATE PROCEDURE searchBookTitle ( p_Title varchar(45))
BEGIN
SELECT * FROM books WHERE Title = p_Title;
END;
