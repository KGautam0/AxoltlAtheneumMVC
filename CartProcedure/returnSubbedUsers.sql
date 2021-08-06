CREATE PROCEDURE returnSubbedUsers()
BEGIN
SELECT * FROM users WHERE promoEmails = 1;
END;
