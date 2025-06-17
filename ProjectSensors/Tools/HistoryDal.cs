using ProjectSensors.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProjectSensors.Tools
{
    public class HistoryDal
    {
        private readonly string _connectionString;

        public HistoryDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(GameHistoryEntry entry)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO game_history(agent_type, weakness_combo, used_sensors, correct_sensors, turns_taken, victory, timestamp) " +
                                     "VALUES(@agent, @weak, @used, @correct, @turns, @victory, @time)";
                    cmd.Parameters.AddWithValue("@agent", entry.AgentType);
                    cmd.Parameters.AddWithValue("@weak", string.Join(",", entry.WeaknessCombination));
                    cmd.Parameters.AddWithValue("@used", string.Join(",", entry.UsedSensors));
                    cmd.Parameters.AddWithValue("@correct", string.Join(",", entry.CorrectSensors));
                    cmd.Parameters.AddWithValue("@turns", entry.TurnsTaken);
                    cmd.Parameters.AddWithValue("@victory", entry.Victory);
                    cmd.Parameters.AddWithValue("@time", entry.Timestamp);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<GameHistoryEntry> GetAll()
        {
            var list = new List<GameHistoryEntry>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT * FROM game_history", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entry = new GameHistoryEntry
                        {
                            AgentType = reader.GetString("agent_type"),
                            WeaknessCombination = new List<SensorType>(),
                            UsedSensors = new List<SensorType>(),
                            CorrectSensors = new List<SensorType>(),
                            TurnsTaken = reader.GetInt32("turns_taken"),
                            Victory = reader.GetBoolean("victory"),
                            Timestamp = reader.GetDateTime("timestamp")
                        };
                        list.Add(entry);
                    }
                }
            }
            return list;
        }
    }
}
