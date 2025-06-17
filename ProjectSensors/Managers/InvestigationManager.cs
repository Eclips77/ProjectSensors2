using ProjectSensors.Entities.AbstractClasses;
using ProjectSensors.Factories;
using ProjectSensors.Tools;
using System;
using System.Collections.Generic;

namespace ProjectSensors.Managers
{
    public class InvestigationManager
    {
        private IranianAgent currentAgent;
        private bool gameRunning = true;
        private int attempts;
        private List<SensorType> _currentSessionUsedSensors;

        public InvestigationManager(IranianAgent agent)
        {
            currentAgent = agent;
            attempts = 0;
            _currentSessionUsedSensors = new List<SensorType>();
        }

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== Welcome to the Investigation Game ===");
            Console.WriteLine($"Target: {currentAgent.Rank}");
            Console.WriteLine($"Required Sensors: {GetRequiredSensorsCount()}");
            Console.WriteLine("----------------------------------------");

            int maxAttemptsForAgent = (currentAgent.Rank == AgentRank.FootSoldier) ? 5 : 10;

            try
            {
                while (gameRunning && attempts < maxAttemptsForAgent)
                {
                    attempts++;
                    currentAgent.OnTurnStart();
                    Console.WriteLine($"\nAttempt {attempts} of {maxAttemptsForAgent}");
                    Console.WriteLine("----------------------------------------");

                    MenuManager.PrintSensorOptions(currentAgent.Rank);

                    int choice = InputHelper.GetNumber("\nEnter sensor number:");
                    SensorType selectedType = MenuManager.GetSensorTypeByChoice(choice);

                    try
                    {
                        Sensor sensor = SensorsFactory.CreateSensor(selectedType);
                        currentAgent.AddSensor(sensor);
                        _currentSessionUsedSensors.Add(selectedType);

                        int correct = currentAgent.Activate();
                        int required = GetRequiredSensorsCount();
                        Console.WriteLine($"\nMatched sensors: {correct}/{required}");

                        if (currentAgent.CheckIfExposed())
                        {
                            Console.WriteLine("\n=== SUCCESS! ===");
                            Console.WriteLine($"Agent {currentAgent.Rank} has been exposed!");
                            Console.WriteLine($"It took you {attempts} attempts.");

                            int baseScore = currentAgent.Rank == AgentRank.FootSoldier ? 100 : 200;
                            int score = baseScore / attempts;
                            GameHistory.Instance.AddSession(new GameSession(
                                PlayerSession.Username,
                                currentAgent.Rank,
                                score,
                                _currentSessionUsedSensors,
                                attempts,
                                true,
                                currentAgent.GetWeaknesses()));

                            try
                            {
                                var conn = Environment.GetEnvironmentVariable("GAME_DB_CONN") ??
                                            "server=localhost;user id=root;password=;database=game";
                                var statsDal = new AgentDal(conn);
                                statsDal.UpdatePlayerStats(PlayerSession.Username, (int)currentAgent.Rank);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error updating player stats: {ex.Message}");
                            }

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();

                            gameRunning = false;
                        }
                        else if (attempts >= maxAttemptsForAgent)
                        {
                            Console.WriteLine("\n=== GAME OVER ===");
                            Console.WriteLine("You've run out of attempts!");
                            Console.WriteLine($"The {currentAgent.Rank} has escaped.");

                            GameHistory.Instance.AddSession(new GameSession(
                                PlayerSession.Username,
                                currentAgent.Rank,
                                0,
                                _currentSessionUsedSensors,
                                attempts,
                                false,
                                currentAgent.GetWeaknesses()));

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();

                            gameRunning = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                        Console.WriteLine("Please try again.");
                        attempts--; 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError in StartGame: {ex.Message}");
                Console.WriteLine("Game cannot continue. Please restart.");
            }
        }

        private int GetRequiredSensorsCount()
        {
            switch (currentAgent.Rank)
            {
                case AgentRank.FootSoldier:
                    return 2;
                case AgentRank.SquadLeader:
                    return 4;
                case AgentRank.SeniorCommander:
                    return 6;
                case AgentRank.OrganizationLeader:
                    return 8;
                default:
                    return 2;
            }
        }

        public void Run()
        {
            StartGame();
        }
    }
}
