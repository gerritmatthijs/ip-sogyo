DROP PROCEDURE IF EXISTS deleteGame;

DELIMITER //

CREATE PROCEDURE deleteGame(IN gameId varchar(255))
BEGIN
	DELETE FROM tichu
    WHERE game_id = gameId;
END //
DELIMITER ;
