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

        public GameTimerProvider(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }
        
        public GameTimer CreateTimer(string name, float targetMilliseconds)
        {
            var timer = new GameTimer(name, targetMilliseconds);
            
            _gameTimers.Add(timer);

            return timer;
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