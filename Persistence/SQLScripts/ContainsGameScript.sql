DROP PROCEDURE IF EXISTS containsGame;

DELIMITER //

CREATE PROCEDURE containsGame(IN gameId varchar(255))
BEGIN
	SELECT COUNT(*) FROM tichu
    WHERE game_id = gameId;
END //
DELIMITER ;