CREATE PROCEDURE searchBookTitle @Title varchar(45)
AS
SELECT * FROM books WHERE Title = @Title
GO;