using System;

namespace ProjectSensors.Tools
{
    public static class InputHelper
    {
        public static int GetNumber(string message)
        {
            Console.Write(message + " ");
            string input = Console.ReadLine();
            int number;
            while (!int.TryParse(input, out number))
            {
                Console.Write("Invalid input. Try again: ");
                input = Console.ReadLine();
            }
            return number;
        }

        public static int GetNumberWithTimeout(string message, int seconds, out bool timedOut)
        {
            DateTime start = DateTime.Now;
            Console.Write(message + " ");
            string input = Console.ReadLine();
            timedOut = (DateTime.Now - start).TotalSeconds > seconds;
            int number;
            while (!int.TryParse(input, out number))
            {
                Console.Write("Invalid input. Try again: ");
                input = Console.ReadLine();
                timedOut = (DateTime.Now - start).TotalSeconds > seconds;
            }
            return number;
        }
    }
}
