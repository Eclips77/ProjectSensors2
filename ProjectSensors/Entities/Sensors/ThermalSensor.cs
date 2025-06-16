using ProjectSensors.Entities.AbstractClasses;

namespace ProjectSensors.Entities.Sensors
{
    public class ThermalSensor : Sensor
    {
        public ThermalSensor()
            : base("Thermal Sensor", SensorType.Thermal)
        {
        }

    }
}
