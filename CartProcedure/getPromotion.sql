CREATE PROCEDURE getPromotion ( p_promotionID INT)
BEGIN
SELECT * FROM promotions WHERE Promotion_ID = p_promotionID;
END;
