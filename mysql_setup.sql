-- SQL script to create the required MySQL database and tables for ProjectSensors

-- Create database if it doesn't exist
CREATE DATABASE IF NOT EXISTS game CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE game;

-- Table for storing player progression
CREATE TABLE IF NOT EXISTS players (
    username VARCHAR(100) NOT NULL,
    highest_rank_unlocked INT NOT NULL,
    PRIMARY KEY (username)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Table for recording game history
CREATE TABLE IF NOT EXISTS game_history (
    username VARCHAR(100) NOT NULL,
    id INT AUTO_INCREMENT PRIMARY KEY,
    agent_type VARCHAR(100) NOT NULL,
    weakness_combo VARCHAR(255) NOT NULL,
    used_sensors VARCHAR(255) NOT NULL,
    correct_sensors VARCHAR(255) NOT NULL,
    turns_taken INT NOT NULL,
    score INT NOT NULL,
    victory TINYINT(1) NOT NULL,
    timestamp DATETIME NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
