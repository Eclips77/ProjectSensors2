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
            // Magnetic sensor logic for counterattack nullification would go here
            // For now, it simply detects if it's a weakness
            return weaknesses.Contains(Type);
        }
    }
} 