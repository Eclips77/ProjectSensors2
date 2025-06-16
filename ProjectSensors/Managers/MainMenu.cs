using System;
using System.Collections.Generic;

namespace ProjectSensors.Tools
{
    public static class MenuManager
    {
        private static readonly Dictionary<int, SensorType> sensorMap = new Dictionary<int, SensorType>
        {
            { 1, SensorType.Audio },
            { 2, SensorType.Thermal }
            // future: { 3, SensorType.Pulse }, ...
        };

        public static void PrintSensorOptions()
        {
            foreach (var kvp in sensorMap)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value}");
            }
        }

        public static SensorType GetSensorTypeByChoice(int choice)
        {
            if (!sensorMap.ContainsKey(choice))
                throw new ArgumentException("Invalid sensor choice!");

            return sensorMap[choice];
        }
    }
}
