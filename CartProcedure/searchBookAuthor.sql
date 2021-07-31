CREATE PROCEDURE searchBookAuthor @Author varchar(45)
AS
SELECT * FROM books WHERE Author_Name = @Author
GO;