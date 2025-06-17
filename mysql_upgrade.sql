-- Optional upgrade script for existing installations
-- Ensures the game_history table has the username column
USE game;
ALTER TABLE game_history ADD COLUMN IF NOT EXISTS username VARCHAR(100) NOT NULL FIRST;
ALTER TABLE game_history ADD COLUMN IF NOT EXISTS score INT NOT NULL AFTER turns_taken;
ALTER TABLE game_history ADD CONSTRAINT IF NOT EXISTS fk_game_history_player FOREIGN KEY (username) REFERENCES players(username);
