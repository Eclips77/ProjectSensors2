using ProjectSensors.Entities.AbstractClasses;
using System.Collections.Generic;

namespace ProjectSensors.Entities.Sensors
{
    public class PulseSensor : Sensor, IBreakableSensor
    {
        private int usesLeft;

        public PulseSensor()
            : base("Pulse Sensor", SensorType.Pulse)
        {
            usesLeft = 3;
        }

        public override bool Activate(List<SensorType> weaknesses)
        {
            if (IsBroken)
                return false;

            usesLeft--;
            if (IsBroken)
            {
                System.Console.WriteLine("Pulse sensor broke!");
            }

            return weaknesses.Contains(Type);
        }

        public bool IsBroken => usesLeft <= 0;
    }
}
