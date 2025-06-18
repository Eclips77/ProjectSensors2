using ProjectSensors.Entities.AbstractClasses;
using System.Collections.Generic;

namespace ProjectSensors.Entities.Sensors
{
    public class MotionSensor : Sensor, IBreakableSensor
    {
        private int usesLeft;

        public MotionSensor()
            : base("Motion Sensor", SensorType.Motion)
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
                System.Console.WriteLine("Motion sensor broke!");
            }

            return weaknesses.Contains(Type);
        }

        public bool IsBroken => usesLeft <= 0;
    }
}
