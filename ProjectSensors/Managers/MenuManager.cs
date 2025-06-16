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
            { 2, AgentRank.SquadLeader }
        };

        private static readonly Dictionary<int, SensorType> sensorMap = new Dictionary<int, SensorType>
        {
            { 1, SensorType.Audio },
            { 2, SensorType.Thermal },
            { 3, SensorType.Pulse }
        };

        private static readonly Dictionary<AgentRank, List<SensorType>> allowedByRank = new Dictionary<AgentRank, List<SensorType>>
        {
            { AgentRank.FootSoldier, new List<SensorType> { SensorType.Audio, SensorType.Thermal } },
            { AgentRank.SquadLeader, new List<SensorType> { SensorType.Audio, SensorType.Thermal, SensorType.Pulse } }
        };

        public static void Show()
        {
            Console.WriteLine("=== IRANIAN AGENT INVESTIGATION GAME ===\n");

            foreach (var option in agentOptions)
            {
                Console.WriteLine($"{option.Key}. {option.Value}");
            }

            int choice = InputHelper.GetNumber("Choose an agent:");
            if (!agentOptions.ContainsKey(choice))
            {
                Console.WriteLine("Invalid choice. Exiting.");
                return;
            }

            AgentRank selectedRank = agentOptions[choice];
            var agent = AgentFactory.CreateAgent(selectedRank);

            var investigation = new InvestigationManager(agent);
            investigation.Run();
        }

        public static void PrintSensorOptions(AgentRank rank)
        {
            Console.WriteLine("Available Sensors:");
            foreach (var kvp in sensorMap)
            {
                if (allowedByRank.ContainsKey(rank) && allowedByRank[rank].Contains(kvp.Value))
                    Console.WriteLine($"{kvp.Key} - {kvp.Value}");
            }
        }

        public static SensorType GetSensorTypeFromChoice(int choice)
        {
            if (!sensorMap.ContainsKey(choice))
                throw new ArgumentException("Invalid sensor choice!");

            return sensorMap[choice];
        }
    }
}