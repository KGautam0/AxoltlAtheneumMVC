CREATE PROCEDURE addPromotion ( p_PromoID INT, p_PromoCode INT, p_PromoStart DATE, p_PromoEnd DATE, p_PromoDiscount DOUBLE)
BEGIN
INSERT INTO promotions(Promotion_ID, Promotion_Code, Promotion_Start, Promotion_End, Promotion_Discount) 
VALUES(p_PromoID, p_PromoCode, p_PromoStart @PromoEnd, @PromoDiscount);
END;
