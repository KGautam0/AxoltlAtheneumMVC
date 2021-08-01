CREATE PROCEDURE searchBookPublisher ( p_Publisher varchar(45))
BEGIN
SELECT * FROM books WHERE Publisher = p_Publisher;
END;
