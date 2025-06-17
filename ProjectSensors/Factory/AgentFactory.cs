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
                case AgentRank.SeniorCommander:
                    return new SeniorCommander(GetRandomWeaknesses(GetAllowedSensorTypes(rank), 6));
                case AgentRank.OrganizationLeader:
                    return new OrganizationLeader(GetRandomWeaknesses(GetAllowedSensorTypes(rank), 8));

                default:
                    throw new ArgumentException($"Unknown Agent Rank: {rank}");
            }
        }

        public static List<SensorType> GetAllowedSensorTypes(AgentRank rank)
        {
            switch (rank)
            {
                case AgentRank.FootSoldier:
                    return new List<SensorType> { SensorType.Audio, SensorType.Thermal };
                case AgentRank.SquadLeader:
                     return new List<SensorType> { SensorType.Audio, SensorType.Thermal, SensorType.Pulse, SensorType.Magnetic, SensorType.Motion };

                default:
                    return Enum.GetValues(typeof(SensorType)).Cast<SensorType>().ToList();
            }
        }

        public static List<SensorType> GetRandomWeaknesses(List<SensorType> pool, int count)
        {
            // Ensure count does not exceed the number of available sensors
            if (count > pool.Count)
            {
                count = pool.Count;
            }

            List<SensorType> shuffledPool = new List<SensorType>(pool);

            // Fisher-Yates shuffle algorithm for true randomness and uniqueness
            int n = shuffledPool.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                SensorType value = shuffledPool[k];
                shuffledPool[k] = shuffledPool[n];
                shuffledPool[n] = value;
            }

            return shuffledPool.Take(count).ToList();
        }
    }
}
