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

        public InvestigationManager(IranianAgent agent)
        {
            currentAgent = agent;
        }

        public void StartGame()
        {
            Console.WriteLine("=== Welcome to the Investigation Game ===");

            try
            {
                while (gameRunning)
                {
                    Console.WriteLine($"\nCurrent Agent Rank: {currentAgent.Rank}");
                    Console.WriteLine("Choose a sensor to attach:");

                    MenuManager.PrintSensorOptions(currentAgent.Rank);

                    int choice = InputHelper.GetNumber("Enter your choice:");
                    SensorType selectedType = MenuManager.GetSensorTypeFromChoice(choice);

                    try
                    {
                        var sensor = SensorsFactory.CreateSensor(selectedType);
                        currentAgent.AttachSensor(sensor);

                        int correct = currentAgent.ActivateSensors();
                        Console.WriteLine($" Matched sensors: {correct}/{2}");

                        if (currentAgent.GetIsExposed())
                        {
                            Console.WriteLine("\n Agent exposed! Well done.");
                            gameRunning = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError: {ex.Message}");
                        Console.WriteLine("Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFatal Error: {ex.Message}");
                Console.WriteLine("Game cannot continue. Please restart.");
            }
        }
        public void Run()
        {
            StartGame();
        }
    }
}
