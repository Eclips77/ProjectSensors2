using ProjectSensors.Entities.AbstractClasses;
using System.Collections.Generic;

namespace ProjectSensors.Entities.AbstractClasses

{
    public abstract class IranianAgent
    {
        public AgentRank Rank { get; protected set; }
        protected List<SensorType> Weaknesses;
        protected List<Sensor> AttachedSensors;
        public bool IsExposed { get; protected set; }
        protected int TurnCounter;

        protected IranianAgent(List<SensorType> weaknesses)
        {
            Weaknesses = weaknesses;
            AttachedSensors = new List<Sensor>();
            IsExposed = false;
            TurnCounter = 0;
        }

        public void AttachSensor(Sensor sensor)
        {
            AttachedSensors.Add(sensor);
        }

        public virtual int ActivateSensors()
        {
            int correct = 0;
            foreach (var sensor in AttachedSensors)
            {
                if (sensor.Activate(Weaknesses))
                    correct++;
            }

            if (correct == Weaknesses.Count)
                IsExposed = true;

            TurnCounter++;
            return correct;
        }

        public bool GetIsExposed()
        {
            return IsExposed;
        }

        public virtual void OnTurnStart() { }
        public virtual void CounterAttack() { }
    }
}
