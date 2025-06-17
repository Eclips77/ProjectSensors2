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
            Console.Write("Enter username: ");
            string user = Console.ReadLine();
            PlayerSession.Login(user);

            var conn = Environment.GetEnvironmentVariable("GAME_DB_CONN") ??
                        "server=localhost;user id=root;password=;database=game";
            var playerDal = new PlayerDal(conn);
            playerDal.EnsurePlayerExists(user);

            MenuManager.StartApplicationLoop();
        }
    }
}
