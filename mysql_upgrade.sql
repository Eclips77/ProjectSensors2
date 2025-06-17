-- Optional upgrade script for existing installations
-- Ensures the game_history table has the username column
USE game;
ALTER TABLE game_history ADD COLUMN IF NOT EXISTS username VARCHAR(100) NOT NULL FIRST;
