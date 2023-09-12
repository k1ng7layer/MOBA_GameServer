using Core.Systems;
using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.GameState;
using Services.GameTimer;
using Settings.TimeSettings;
using Systems.Abstract;
using Utils;

namespace Systems
{
    public class CharacterPickFinalCountdownSystem : AGameStateSystem, 
        IUpdateSystem
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly IGameTimerProvider _gameTimerProvider;
        private readonly ITimeSettings _timeSettings;
        private readonly INetworkServerManager _networkServerManager;

        public CharacterPickFinalCountdownSystem(
            IGameStateProvider gameStateProvider, 
            IGameTimerProvider gameTimerProvider,
            ITimeSettings timeSettings,
            INetworkServerManager networkServerManager
        ) : base(gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
            _gameTimerProvider = gameTimerProvider;
            _timeSettings = timeSettings;
            _networkServerManager = networkServerManager;
        }

        public override EGameState GameState => EGameState.PreparingAfterPick;
        
        protected override void OnStateChanged()
        {
            var finalTimer =
                _gameTimerProvider.CreateTimer(TimerNames.PickFinalTimer, _timeSettings.CharacterPickFinalStateTime);

            finalTimer.Elapsed += BeginLoadingState;
        }
        
        private void BeginLoadingState()
        {
            _gameStateProvider.SetState(EGameState.ClientLoading);
        }

        public void Update()
        {
            var hasTimer = _gameTimerProvider.TryGet(TimerNames.PickFinalTimer, out var timer);
            
            if(!hasTimer)
                return;
            
            _networkServerManager.SendMessage(new CharacterPickFinalTimer(timer.Value));
        }
    }
}