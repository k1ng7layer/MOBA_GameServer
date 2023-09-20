using System;
using Core.Systems;
using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.TimeProvider;
using Settings.TimeSettings;

namespace Systems
{
    public class CharacterPickTimerSystem :
        IUpdateSystem
        //ICharacterPickTimerProvider
    {
        private readonly ITimeProvider _timeProvider;
        private readonly ITimeSettings _timeSettings;
        private readonly INetworkServerManager _networkServerManager;
        private bool _running;

        public CharacterPickTimerSystem(ITimeProvider timeProvider,
            ITimeSettings timeSettings,
            INetworkServerManager networkServerManager
        )
        {
            _timeProvider = timeProvider;
            _timeSettings = timeSettings;
            _networkServerManager = networkServerManager;
        }

        public event Action Elapsed;
        
        public void StartTimer()
        {
            _running = true;
        }

        public void StartFinalCountdown()
        {
            
        }

        public float Value { get; private set; }

        public void Update()
        {
            if(!_running)
                return;

            if (Value >= _timeSettings.CharacterPickTime)
            {
                Elapsed?.Invoke();
                _running = false;
            }
            
            Value += _timeProvider.DeltaTime;
            
            _networkServerManager.SendMessage(new CharacterPickTimerMessage(Value));
        }
    }
}