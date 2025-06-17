using ProjectSensors.Entities.AbstractClasses;
using System;

namespace ProjectSensors.Entities.Sensors
{
    public class SignalSensor : Sensor
    {
        public SignalSensor() : base("Signal Sensor", SensorType.Signal)
        {
        }

        public override bool Activate(System.Collections.Generic.List<SensorType> weaknesses)
        {
            bool matched = base.Activate(weaknesses);
            if (matched)
            {
                Console.WriteLine("Signal sensor reveals agent rank.");
            }
            return matched;
        }
    }
}
