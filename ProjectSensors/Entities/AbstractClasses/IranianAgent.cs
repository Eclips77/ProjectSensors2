using ProjectSensors.Entities.AbstractClasses;
using System.Collections.Generic;

namespace ProjectSensors.Entities.AbstractClasses

{
    public abstract class IranianAgent
    {
        public AgentRank Rank { get; protected set; }
        protected List<SensorType> WeaknessCombination;
        protected List<Sensor> AttachedSensors;
        public bool IsExposed { get; protected set; }
        protected int TurnCounter;
        protected int ActivateCounter;

        protected IranianAgent(List<SensorType> weaknesses)
        {
            WeaknessCombination = weaknesses;
            AttachedSensors = new List<Sensor>();
            IsExposed = false;
            TurnCounter = 0;
            ActivateCounter = 0;
        }

        public void AddSensor(Sensor sensor)
        {
            AttachedSensors.Add(sensor);
        }

        public virtual int Activate()
        {
            int correct = 0;
            foreach (Sensor sensor in AttachedSensors)
            {
                if (sensor.Activate(WeaknessCombination))
                    correct++;
            }

            if (correct == WeaknessCombination.Count)
                IsExposed = true;

            TurnCounter++;
            ActivateCounter++;
            return correct;
        }

        public bool CheckIfExposed()
        {
            return IsExposed;
        }

        public virtual void OnTurnStart() { }
        public virtual void CounterAttack() { }
        public virtual void ResetSensorsAndWeaknessList() { }
    }
}
