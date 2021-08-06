CREATE PROCEDURE getPromotion ( p_promotionCode VARCHAR(45))
BEGIN
SELECT * FROM promotions WHERE Promotion_Codee = p_promotionCode;
END;
