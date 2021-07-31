CREATE PROCEDURE searchBookCategory @Category varchar(45)
AS
SELECT * FROM books WHERE Category = @Category
GO;