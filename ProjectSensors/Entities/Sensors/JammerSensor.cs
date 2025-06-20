using ProjectSensors.Entities.AbstractClasses;
using System;
using System.Collections.Generic;

namespace ProjectSensors.Entities.Sensors
{
    public class JammerSensor : Sensor, IBreakableSensor
    {
        private bool _used;
        private static readonly Random rnd = new Random();

        public JammerSensor() : base("Jammer Sensor", SensorType.Jammer)
        {
            _used = false;
        }

        public override bool Activate(List<SensorType> weaknesses)
        {
            if (_used)
                return false;
            _used = true;
            if (weaknesses.Count > 0)
            {
                int index = rnd.Next(weaknesses.Count);
                weaknesses.RemoveAt(index);
                Console.WriteLine("Jammer sensor disabled one of the agent weaknesses!");
            }
            return false; // jammer does not directly detect
        }

        public bool IsBroken => _used;
    }
}
