﻿using ProjectSensors.Entities.AbstractClasses;
using System.Collections.Generic;
using System;
using ProjectSensors.Enums;

namespace ProjectSensors.Entities.AbstractClasses

{
    public abstract class IranianAgent
    {
        public AgentRank Rank { get; protected set; }
        protected List<SensorType> WeaknessCombination;
        protected List<Sensor> AttachedSensors;
        public bool IsExposed { get; protected set; }
        public AgentMood Mood { get; protected set; }
        protected int TurnCounter;
        protected int ActivateCounter;

        public List<SensorType> GetWeaknesses()
        {
            return new List<SensorType>(WeaknessCombination);
        }

        protected IranianAgent(List<SensorType> weaknesses)
        {
            WeaknessCombination = weaknesses;
            AttachedSensors = new List<Sensor>();
            IsExposed = false;
            TurnCounter = 0;
            ActivateCounter = 0;
            Mood = AgentMood.Calm;
        }

        public void AddSensor(Sensor sensor)
        {
            AttachedSensors.Add(sensor);
        }

        public virtual int Activate()
        {
            var matchedTypes = new HashSet<SensorType>();
            for (int i = AttachedSensors.Count - 1; i >= 0; i--)
            {
                var sensor = AttachedSensors[i];
                if (sensor.Activate(WeaknessCombination))
                    matchedTypes.Add(sensor.Type);

                if (sensor is IBreakableSensor br && br.IsBroken)
                {
                    AttachedSensors.RemoveAt(i);
                    Console.WriteLine($"{sensor.Name} was removed after breaking.");
                }
            }

            int required = new HashSet<SensorType>(WeaknessCombination).Count;
            double ratio = (double)matchedTypes.Count / required;
            if (ratio >= 1)
                Mood = AgentMood.Panicked;
            else if (ratio >= 0.75)
                Mood = AgentMood.Nervous;
            else if (ratio >= 0.5)
                Mood = AgentMood.Alert;
            else
                Mood = AgentMood.Calm;

            ConsoleColor prev = Console.ForegroundColor;
            ConsoleColor moodColor = ConsoleColor.White;
            switch (Mood)
            {
                case AgentMood.Calm:
                    moodColor = ConsoleColor.Green;
                    break;
                case AgentMood.Alert:
                    moodColor = ConsoleColor.Yellow;
                    break;
                case AgentMood.Nervous:
                    moodColor = ConsoleColor.DarkYellow;
                    break;
                case AgentMood.Panicked:
                    moodColor = ConsoleColor.Red;
                    break;
            }
            Console.ForegroundColor = moodColor;
            Console.WriteLine($"Agent mood: {Mood}");
            Console.ForegroundColor = prev;

            if (matchedTypes.Count == required)
                IsExposed = true;

            TurnCounter++;
            ActivateCounter++;
            return matchedTypes.Count;
        }

        public bool CheckIfExposed()
        {
            return IsExposed;
        }

        public virtual void OnTurnStart() { }
        public virtual void CounterAttack() { }
        public virtual void ResetSensorsAndWeaknessList() { }
    }
}
