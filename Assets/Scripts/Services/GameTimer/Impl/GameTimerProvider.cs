using System;
using System.Collections.Generic;
using Core.Systems;
using Services.TimeProvider;

namespace Services.GameTimer.Impl
{
    public class GameTimerProvider : IGameTimerProvider, 
        IUpdateSystem
    {
        private readonly ITimeProvider _timeProvider;
        private readonly List<GameTimer> _gameTimers = new();
        private readonly Dictionary<string, GameTimer> _gameTimersTable = new();

        public GameTimerProvider(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }
        
        public GameTimer CreateTimer(string name, float targetMilliseconds)
        {
            if (_gameTimersTable.ContainsKey(name))
                throw new InvalidOperationException(
                    $"[{nameof(GameTimerProvider)}] timer with id {name} already existed");
            
            var timer = new GameTimer(name, targetMilliseconds);
            
            _gameTimers.Add(timer);
            
            _gameTimersTable.Add(name, timer);
            
            return timer;
        }

        public bool TryGet(string name, out GameTimer timer)
        {
            return _gameTimersTable.TryGetValue(name, out timer);
        }

        public void Update()
        {
            foreach (var timer in _gameTimers)
            {
                if(!timer.Running)
                    continue;
                
                timer.Increase(_timeProvider.DeltaTime);
            }
        }
    }
}