using ProjectSensors.Entities.AbstractClasses;
using ProjectSensors.Enums;
using ProjectSensors.Factories;
using ProjectSensors.Tools;
using System;

namespace ProjectSensors.Managers
{
    public class ParallelInvestigationManager
    {
        private readonly InvestigationManager _first;
        private readonly InvestigationManager _second;

        public ParallelInvestigationManager(IranianAgent a, IranianAgent b, Difficulty diff)
        {
            _first = new InvestigationManager(a, diff);
            _second = new InvestigationManager(b, diff);
        }

        public void Run()
        {
            Console.WriteLine("=== Parallel Investigation ===");
            bool firstTurn = true;
            while (true)
            {
                if (firstTurn)
                    _first.StartGame();
                else
                    _second.StartGame();
                firstTurn = !firstTurn;
                Console.WriteLine("Continue with next agent? y/n");
                if (!Console.ReadLine().Equals("y", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }
}
