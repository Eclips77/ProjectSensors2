using ProjectSensors.Entities.AbstractClasses;
using ProjectSensors.Factories;
using System;
using System.Collections.Generic;

namespace ProjectSensors.Entities.Agents
{
    public class OrganizationLeader : IranianAgent
    {
        private readonly Random random = new Random();

        public OrganizationLeader(List<SensorType> weaknesses)
            : base(weaknesses)
        {
            Rank = AgentRank.OrganizationLeader;
        }

        public override void OnTurnStart()
        {
            if (TurnCounter > 0 && TurnCounter % 3 == 0)
            {
                CounterAttack();
            }
            if (TurnCounter > 0 && TurnCounter % 10 == 0)
            {
                ResetSensorsAndWeaknessList();
                Console.WriteLine("Organization Leader reset weaknesses and sensors.");
            }
        }

        public override void CounterAttack()
        {
            if (AttachedSensors.Count == 0)
                return;
            int index = random.Next(AttachedSensors.Count);
            AttachedSensors.RemoveAt(index);
            Console.WriteLine("Organization Leader counterattacked and removed one sensor.");
        }

        public override void ResetSensorsAndWeaknessList()
        {
            AttachedSensors.Clear();
            WeaknessCombination = AgentFactory.GetRandomWeaknesses(AgentFactory.GetAllowedSensorTypes(Rank), GetRequiredSensors());
        }

        private int GetRequiredSensors()
        {
            return 8;
        }
    }
}
