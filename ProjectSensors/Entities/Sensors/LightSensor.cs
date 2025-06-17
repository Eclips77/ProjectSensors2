using ProjectSensors.Entities.AbstractClasses;
using System;

namespace ProjectSensors.Entities.Sensors
{
    public class LightSensor : Sensor
    {
        public LightSensor() : base("Light Sensor", SensorType.Light)
        {
        }

        public override bool Activate(System.Collections.Generic.List<SensorType> weaknesses)
        {
            bool matched = base.Activate(weaknesses);
            if (matched)
            {
                Console.WriteLine("Light sensor reveals multiple agent properties.");
            }
            return matched;
        }
    }
}
