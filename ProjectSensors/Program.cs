using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSensors.Managers;
using ProjectSensors.Tools;

namespace ProjectSensors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string user;
                do
                {
                    Console.Write("Enter username: ");
                    user = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(user))
                    {
                        Console.WriteLine("Username cannot be empty. Please try again.");
                    }
                } while (string.IsNullOrWhiteSpace(user));

                PlayerSession.Login(user);

                var conn = Environment.GetEnvironmentVariable("GAME_DB_CONN") ??
                            "server=localhost;user id=root;password=;database=game";
                var playerDal = new PlayerDal(conn);
                try
                {
                    playerDal.EnsurePlayerExists(user);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in EnsurePlayerExists: {ex.Message}");
                }

                MenuManager.StartApplicationLoop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled error in Main: {ex.Message}");
            }
        }
    }
}
