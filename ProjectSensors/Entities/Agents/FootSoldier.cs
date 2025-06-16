using System.Collections.Generic;

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
