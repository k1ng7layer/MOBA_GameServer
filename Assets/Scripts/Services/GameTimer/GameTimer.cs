using System;
using UnityEngine;

namespace Services.GameTimer
{
    public class GameTimer
    {
        private readonly float _targetMilliseconds;

        public GameTimer(string name, float targetMilliseconds)
        {
            _targetMilliseconds = targetMilliseconds;
            Name = name;
        }
        
        public float Value { get; private set; }
        public bool Running { get; private set; }
        public string Name { get; }
        public event Action Elapsed;

        public void Increase(float deltaTime)
        {
            Value += deltaTime;
            Value = Mathf.Clamp(Value, 0, _targetMilliseconds);
            
            if (Value >= _targetMilliseconds)
            {
                Running = false;
                Elapsed?.Invoke();
                Elapsed = null;
            }
        }

        public void Start()
        {
            Running = true;
        }

        public void Stop()
        {
            Running = false;
        }
    }
}