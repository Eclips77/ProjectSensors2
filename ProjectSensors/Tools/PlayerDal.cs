using MySql.Data.MySqlClient;
using System;

namespace ProjectSensors.Tools
{
    public class PlayerDal
    {
        private readonly string _connectionString;

        public PlayerDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void EnsurePlayerExists(string username)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT IGNORE INTO players(username, highest_rank_unlocked) VALUES(@user, 0);";
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PlayerDal.EnsurePlayerExists: {ex.Message}");
            }
        }
    }
}
