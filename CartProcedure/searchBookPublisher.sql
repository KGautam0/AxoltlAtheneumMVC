CREATE PROCEDURE searchBookPublisher @Publisher varchar(45)
AS
SELECT * FROM books WHERE Publisher = @Publisher
GO;