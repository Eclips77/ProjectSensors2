using ProjectSensors.Entities.AbstractClasses;

namespace ProjectSensors.Entities.Sensors
{
    public class AudioSensor : Sensor
    {
        public AudioSensor()
            : base("Audio Sensor", SensorType.Audio)
        {
        }

    }
}
