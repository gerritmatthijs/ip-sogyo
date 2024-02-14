DROP PROCEDURE IF EXISTS updateGame;

DELIMITER //

CREATE PROCEDURE updateGame(IN gameId varchar(256), IN playerHands varchar(255), IN leaderName varchar(100), 
	IN lastPlayed varchar(100), IN nextTurn int)
BEGIN
	UPDATE tichu SET player_hands = playerHands, leader_name = leaderName, last_played = lastPlayed, turn = nextTurn
    WHERE game_id = gameId;
END //
DELIMITER ;