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
                    Console.WriteLine($"\nAttempt {attempts} of {maxAttemptsForAgent}");
                    Console.WriteLine("----------------------------------------");

                    MenuManager.PrintSensorOptions(currentAgent.Rank);

                    int choice = InputHelper.GetNumber("\nEnter sensor number:");
                    SensorType selectedType = MenuManager.GetSensorTypeByChoice(choice);

                    try
                    {
                        Sensor sensor = SensorsFactory.CreateSensor(selectedType);
                        currentAgent.AttachSensor(sensor);
                        _currentSessionUsedSensors.Add(selectedType);

                        int correct = currentAgent.ActivateSensors();
                        int required = GetRequiredSensorsCount();
                        Console.WriteLine($"\nMatched sensors: {correct}/{required}");

                        if (currentAgent.GetIsExposed())
                        {
                            Console.WriteLine("\n=== SUCCESS! ===");
                            Console.WriteLine($"Agent {currentAgent.Rank} has been exposed!");
                            Console.WriteLine($"It took you {attempts} attempts.");

                            int baseScore = currentAgent.Rank == AgentRank.FootSoldier ? 100 : 200;
                            int score = baseScore / attempts;
                            GameHistory.Instance.AddSession(new GameSession(currentAgent.Rank, score, _currentSessionUsedSensors));

                            gameRunning = false;
                        }
                        else if (attempts >= maxAttemptsForAgent)
                        {
                            Console.WriteLine("\n=== GAME OVER ===");
                            Console.WriteLine("You've run out of attempts!");
                            Console.WriteLine($"The {currentAgent.Rank} has escaped.");

                            GameHistory.Instance.AddSession(new GameSession(currentAgent.Rank, 0, _currentSessionUsedSensors));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                        Console.WriteLine("Please try again.");
                        attempts--; 
                    }
                }

                if (!currentAgent.GetIsExposed() || attempts >= maxAttemptsForAgent)
                {
                     GameHistory.Instance.DisplayHistory();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFatal Error: {ex.Message}");
                Console.WriteLine("Game cannot continue. Please restart.");
            }
        }

        private int GetRequiredSensorsCount()
        {
            return currentAgent.Rank == AgentRank.FootSoldier ? 2 : 4;
        }

        public void Run()
        {
            StartGame();
        }
    }
}
