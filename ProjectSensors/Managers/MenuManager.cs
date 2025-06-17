using System;
using System.Collections.Generic;
using ProjectSensors.Entities.AbstractClasses;
using ProjectSensors.Factories;
using ProjectSensors.Tools;

namespace ProjectSensors.Managers
{
    public static class MenuManager
    {
        private static readonly Dictionary<int, AgentRank> agentOptions = new Dictionary<int, AgentRank>
        {
            { 1, AgentRank.FootSoldier },
            { 2, AgentRank.SquadLeader },
            { 3, AgentRank.SeniorCommander },
            { 4, AgentRank.OrganizationLeader }
        };

        private static readonly Dictionary<int, SensorType> sensorMap = new Dictionary<int, SensorType>
        {
            { 1, SensorType.Audio },
            { 2, SensorType.Thermal },
            { 3, SensorType.Pulse },
            { 4, SensorType.Magnetic },
            { 5, SensorType.Motion },
            { 6, SensorType.Signal },
            { 7, SensorType.Light }
        };

        private static readonly Dictionary<AgentRank, List<SensorType>> allowedByRank = new Dictionary<AgentRank, List<SensorType>>
        {
            { AgentRank.FootSoldier, new List<SensorType> { SensorType.Audio, SensorType.Thermal } },
            { AgentRank.SquadLeader, new List<SensorType> { SensorType.Audio, SensorType.Thermal, SensorType.Pulse, SensorType.Magnetic, SensorType.Motion } },
            { AgentRank.SeniorCommander, new List<SensorType> { SensorType.Audio, SensorType.Thermal, SensorType.Pulse, SensorType.Magnetic, SensorType.Motion, SensorType.Signal } },
            { AgentRank.OrganizationLeader, new List<SensorType> { SensorType.Audio, SensorType.Thermal, SensorType.Pulse, SensorType.Magnetic, SensorType.Motion, SensorType.Signal, SensorType.Light } }
        };

        public static bool StartApplicationLoop()
        {
            bool exitApp = false;
            bool logout = false;
            while (!exitApp && !logout)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("=== IRANIAN AGENT INVESTIGATION GAME ===");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("1. Start New Investigation");
                    Console.WriteLine("2. View Game History");
                    Console.WriteLine("3. Log Out");
                    Console.WriteLine("4. Exit");
                    Console.WriteLine("----------------------------------------");

                    int choice = InputHelper.GetNumber("Enter your choice:");

                    switch (choice)
                    {
                        case 1:
                            DisplayAgentSelectionMenu();
                            break;
                        case 2:
                            if (PlayerSession.IsAdmin)
                            {
                                DisplayAdminHistoryMenu();
                            }
                            else
                            {
                                GameHistory.Instance.DisplayHistory(PlayerSession.Username);
                            }
                            break;
                        case 3:
                            logout = true;
                            break;
                        case 4:
                            exitApp = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in StartApplicationLoop: {ex.Message}");
                }
            }
            Console.WriteLine("Thank you for playing!");
            return logout;
        }

        private static void DisplayAgentSelectionMenu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== IRANIAN AGENT INVESTIGATION GAME ===");
                Console.WriteLine("Choose your target:");
                Console.WriteLine("----------------------------------------");

                foreach (var option in agentOptions)
                {
                    Console.WriteLine($"{option.Key}. {option.Value}");
                    string required = "2";
                    if (option.Value == AgentRank.SquadLeader) required = "4";
                    else if (option.Value == AgentRank.SeniorCommander) required = "6";
                    else if (option.Value == AgentRank.OrganizationLeader) required = "8";
                    Console.WriteLine($"   Required Sensors: {required}");
                    Console.WriteLine($"   Available Sensors: {string.Join(", ", allowedByRank[option.Value])}");
                    Console.WriteLine();
                }

                int choice = InputHelper.GetNumber("Enter target number (1-4):");
                if (!agentOptions.ContainsKey(choice))
                {
                    Console.WriteLine("Invalid choice. Returning to main menu.");
                    return;
                }

                AgentRank selectedRank = agentOptions[choice];
                var agent = AgentFactory.CreateAgent(selectedRank);

                var investigation = new InvestigationManager(agent);
                investigation.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DisplayAgentSelectionMenu: {ex.Message}");
            }
        }

        private static void DisplayAdminHistoryMenu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== HISTORY MENU ===");
                Console.WriteLine("1. View All History");
                Console.WriteLine("2. View History By Player");
                Console.WriteLine("3. Back");

                int choice = InputHelper.GetNumber("Enter your choice:");

                switch (choice)
                {
                    case 1:
                        GameHistory.Instance.DisplayHistory();
                        break;
                    case 2:
                        var conn = Environment.GetEnvironmentVariable("GAME_DB_CONN") ??
                                    "server=localhost;user id=root;password=;database=game";
                        var playerDal = new PlayerDal(conn);
                        var users = playerDal.GetAllUsernames();
                        for (int i = 0; i < users.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {users[i]}");
                        }
                        int userChoice = InputHelper.GetNumber("Select player number:");
                        if (userChoice > 0 && userChoice <= users.Count)
                            GameHistory.Instance.DisplayHistory(users[userChoice - 1]);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DisplayAdminHistoryMenu: {ex.Message}");
            }
        }

        public static void PrintSensorOptions(AgentRank rank)
        {
            Console.WriteLine("\nAvailable Sensors for this target:");
            Console.WriteLine("----------------------------------------");
            foreach (var kvp in sensorMap)
            {
                if (allowedByRank.ContainsKey(rank) && allowedByRank[rank].Contains(kvp.Value))
                {
                    Console.WriteLine($"{kvp.Key}. {kvp.Value}");
                }
            }
        }

        public static SensorType GetSensorTypeByChoice(int choice)
        {
            if (!sensorMap.ContainsKey(choice))
                throw new ArgumentException("Invalid sensor choice! Please select a valid number.");

            return sensorMap[choice];
        }
    }
}