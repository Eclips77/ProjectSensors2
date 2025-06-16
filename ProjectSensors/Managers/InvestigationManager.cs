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

        public void StartGame()
        {
            Console.WriteLine("=== Welcome to the Investigation Game ===");

            currentAgent = AgentFactory.CreateAgent(AgentRank.FootSoldier);

            while (gameRunning)
            {
                Console.WriteLine($"\nCurrent Agent Rank: {currentAgent.Rank}");
                Console.WriteLine("Choose a sensor to attach:");

                MenuManager.PrintSensorOptions();

                int choice = InputHelper.GetNumber("Enter your choice:");
                SensorType selectedType = MenuManager.GetSensorTypeByChoice(choice);

                var sensor = SensorFactory.CreateSensor(selectedType);
                currentAgent.AttachSensor(sensor);

                int correct = currentAgent.ActivateSensors();
                Console.WriteLine($" Matched sensors: {correct}/{2}");

                if (currentAgent.GetIsExposed())
                {
                    Console.WriteLine("\n Agent exposed! Well done.");
                    gameRunning = false;
                }
            }
        }
        public void Run()
        {
            StartGame();
        }
    }
}
