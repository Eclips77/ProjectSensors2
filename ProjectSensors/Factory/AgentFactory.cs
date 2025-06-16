using ProjectName.Entities.Agents;
using ProjectSensors.Entities.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectSensors.Factories
{
    public static class AgentFactory
    {
        private static Random random = new Random();

        public static IranianAgent CreateAgent(AgentRank rank)
        {
            switch (rank)
            {
                case AgentRank.FootSoldier:
                    return new FootSoldier(GetRandomWeaknesses(2));
        
                default:
                    throw new ArgumentException($"Unknown Agent Rank: {rank}");
            }
        }

        private static List<SensorType> GetRandomWeaknesses(int count)
        {
            var sensorTypes = Enum.GetValues(typeof(SensorType)).Cast<SensorType>().ToList();
            List<SensorType> result = new List<SensorType>();

            for (int i = 0; i < count; i++)
            {
                int index = random.Next(sensorTypes.Count);
                result.Add(sensorTypes[index]);
            }

            return result;
        }
    }
}
