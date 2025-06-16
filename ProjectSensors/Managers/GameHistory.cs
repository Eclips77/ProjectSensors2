using System;
using System.Collections.Generic;
using System.Linq;
using ProjectSensors.Entities.AbstractClasses;
using ProjectSensors.Tools;

namespace ProjectSensors.Managers
{
    public class GameHistory
    {
        private static GameHistory _instance;
        private readonly List<GameSession> _sessions;
        private int _totalScore;
        private int _gamesPlayed;
        private readonly Dictionary<AgentRank, int> _agentsExposed;
        private readonly Dictionary<SensorType, int> _sensorsUsed;

        private GameHistory()
        {
            _sessions = new List<GameSession>();
            _totalScore = 0;
            _gamesPlayed = 0;
            _agentsExposed = new Dictionary<AgentRank, int>();
            _sensorsUsed = new Dictionary<SensorType, int>();
        }

        public static GameHistory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameHistory();
                }
                return _instance;
            }
        }

        public void AddSession(GameSession session)
        {
            _sessions.Add(session);
            _totalScore += session.Score;
            _gamesPlayed++;

            if (!_agentsExposed.ContainsKey(session.AgentRank))
            {
                _agentsExposed[session.AgentRank] = 0;
            }
            _agentsExposed[session.AgentRank]++;

            foreach (var sensor in session.UsedSensors)
            {
                if (!_sensorsUsed.ContainsKey(sensor))
                {
                    _sensorsUsed[sensor] = 0;
                }
                _sensorsUsed[sensor]++;
            }
        }

        public void DisplayHistory()
        {
            Console.Clear();
            Console.WriteLine("=== GAME HISTORY ===");
            Console.WriteLine($"Total Games Played: {_gamesPlayed}");
            Console.WriteLine($"Total Score: {_totalScore}");
            Console.WriteLine($"Average Score: {(_gamesPlayed > 0 ? _totalScore / _gamesPlayed : 0)}");
            Console.WriteLine("\nAgents Exposed:");
            foreach (var agent in _agentsExposed)
            {
                Console.WriteLine($"- {agent.Key}: {agent.Value} times");
            }
            Console.WriteLine("\nSensors Used:");
            foreach (var sensor in _sensorsUsed)
            {
                Console.WriteLine($"- {sensor.Key}: {sensor.Value} times");
            }
            Console.WriteLine("\nRecent Sessions:");
            foreach (var session in _sessions.Skip(Math.Max(0, _sessions.Count - 5)).ToList())
            {
                Console.WriteLine($"\nDate: {session.Date}");
                Console.WriteLine($"Agent: {session.AgentRank}");
                Console.WriteLine($"Score: {session.Score}");
                Console.WriteLine($"Sensors Used: {string.Join(", ", session.UsedSensors)}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public class GameSession
    {
        public DateTime Date { get; }
        public AgentRank AgentRank { get; }
        public int Score { get; }
        public List<SensorType> UsedSensors { get; }

        public GameSession(AgentRank agentRank, int score, List<SensorType> usedSensors)
        {
            Date = DateTime.Now;
            AgentRank = agentRank;
            Score = score;
            UsedSensors = new List<SensorType>(usedSensors);
        }
    }
} 