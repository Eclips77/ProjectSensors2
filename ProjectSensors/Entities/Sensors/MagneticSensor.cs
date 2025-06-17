using ProjectSensors.Entities.AbstractClasses;
using System.Collections.Generic;

namespace ProjectSensors.Entities.Sensors
{
    public class MagneticSensor : Sensor
    {
        public MagneticSensor()
            : base("Magnetic Sensor", SensorType.Magnetic)
        {
        }

        public override bool Activate(List<SensorType> weaknesses)
        {
            return weaknesses.Contains(Type);
        }
    }
} 

