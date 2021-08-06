CREATE PROCEDURE returnSubbedUsers()
BEGIN
SELECT * FROM users WHERE isSubscribed = 1;
END;
