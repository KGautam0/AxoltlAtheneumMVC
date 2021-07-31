CREATE PROCEDURE addPromotion @PromoID INT, @PromoCode INT, @PromoStart DATE, @PromoEnd DATE, @PromoDiscount DOUBLE
AS
INSERT INTO promotions VALUES(@PromoID, @PromoCode, @PromoStart @PromoEnd, @PromoDiscount)
GO;