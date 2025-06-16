using System.Collections.Generic;

namespace ProjectName.AbstractClasses
{
    public abstract class Sensor
    {
        public string Name { get; protected set; }
        public SensorType Type { get; protected set; }

        protected Sensor(string name, SensorType type)
        {
            Name = name;
            Type = type;
        }

        public virtual bool Activate(List<SensorType> weaknesses)
        {
            return weaknesses.Contains(Type);
        }
    }

}

