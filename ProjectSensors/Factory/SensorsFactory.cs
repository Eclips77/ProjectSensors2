using ProjectSensors.Entities.AbstractClasses;
using ProjectSensors.Entities.Sensors;
using System;

namespace ProjectSensors.Factories
{
    public static class SensorFactory
    {
        public static Sensor CreateSensor(SensorType type)
        {
            switch (type)
            {
                case SensorType.Thermal:
                    return new ThermalSensor();
                case SensorType.Audio:
                    return new AudioSensor();
              
                default:
                    throw new ArgumentException($"Unknown Sensor Type: {type}");
            }
        }
    }
}
