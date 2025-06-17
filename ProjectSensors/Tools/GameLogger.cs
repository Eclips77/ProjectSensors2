using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectSensors.Tools
{
    public static class GameLogger
    {
        private static readonly List<string> _lines = new List<string>();
        public static void Clear() => _lines.Clear();
        public static void Log(string line)
        {
            _lines.Add(line);
        }
        public static void Save(string fileName)
        {
            try
            {
                File.WriteAllLines(fileName, _lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save log: {ex.Message}");
            }
        }
    }
}
