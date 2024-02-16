DROP PROCEDURE IF EXISTS saveGame;

DELIMITER //

CREATE PROCEDURE saveGame(IN gameId varchar(255), IN playerNames varchar(255), IN playerHands varchar(255), 
	IN leaderName varchar(100), IN lastPlayed varchar(100), IN turn int)
BEGIN
	INSERT INTO tichu
    VALUES(gameId, playerNames, playerHands, leaderName, lastPlayed, turn);
END //
DELIMITER ;