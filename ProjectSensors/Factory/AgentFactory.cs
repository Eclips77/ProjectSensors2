using ProjectSensors.Entities.AbstractClasses;
using ProjectSensors.Entities.Agents;
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
                    return new FootSoldier(GetRandomWeaknesses(GetAllowedSensorTypes(rank), 2));
                case AgentRank.SquadLeader:
                    return new SquadLeader(GetRandomWeaknesses(GetAllowedSensorTypes(rank), 4));

                default:
                    throw new ArgumentException($"Unknown Agent Rank: {rank}");
            }
        }

        private static List<SensorType> GetAllowedSensorTypes(AgentRank rank)
        {
            switch (rank)
            {
                case AgentRank.FootSoldier:
                    return new List<SensorType> { SensorType.Audio, SensorType.Thermal };
                case AgentRank.SquadLeader:
                     return new List<SensorType> { SensorType.Audio, SensorType.Thermal, SensorType.Pulse };

                default:
                    return Enum.GetValues(typeof(SensorType)).Cast<SensorType>().ToList();
            }
        }

        private static List<SensorType> GetRandomWeaknesses(List<SensorType> pool, int count)
        {
            List<SensorType> result = new List<SensorType>();

            for (int i = 0; i < count; i++)
            {
                int index = random.Next(pool.Count);
                result.Add(pool[index]);
            }

            return result;
        }
    }
}
