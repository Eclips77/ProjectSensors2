using System.Collections.Generic;
using ProjectSensors.Entities.AbstractClasses;


namespace ProjectName.Entities.Agents
{
    public class FootSoldier : IranianAgent
    {
        public FootSoldier(List<SensorType> weaknesses)
            : base(weaknesses)
        {
            Rank = AgentRank.FootSoldier;
        }

    }
}
