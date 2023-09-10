using System;
using Core.Systems;
using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.CharacterPick;
using Services.GameState;
using Services.GameTimer;
using Settings.TimeSettings;
using Systems.Abstract;

namespace Systems
{
    public class CharacterPickSystem : AGameStateSystem, 
        IUpdateSystem
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly ICharacterPickTimerProvider _characterPickTimerProvider;
        private readonly INetworkServerManager _serverManager;
        private readonly IGameTimerProvider _gameTimerProvider;
        private readonly ITimeSettings _timeSettings;

        public CharacterPickSystem(
            IGameStateProvider gameStateProvider, 
            ICharacterPickTimerProvider characterPickTimerProvider,
            INetworkServerManager serverManager,
            IGameTimerProvider gameTimerProvider,
            ITimeSettings timeSettings
        ) : base(gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
            _characterPickTimerProvider = characterPickTimerProvider;
            _serverManager = serverManager;
            _gameTimerProvider = gameTimerProvider;
            _timeSettings = timeSettings;
        }

        protected override EGameState GameState => EGameState.CharacterPick;
        
        protected override void OnGameStateChanged()
        {
            _characterPickTimerProvider.StartTimer();
            _characterPickTimerProvider.Elapsed += BeginFinalCountdown;
            
            _serverManager.RegisterMessageHandler<CharacterSelectMessage>(OnPlayerCharacterSelect);
            _serverManager.RegisterMessageHandler<CharacterPickMessage>(OnCharacterPickAccepted);
        }

        public void Update()
        {
            var timer = _characterPickTimerProvider.Value;
            
            _serverManager.SendMessage(new CharacterPickTimerMessage(timer));
        }

        private void BeginFinalCountdown()
        {
            var finalTimer =
                _gameTimerProvider.CreateTimer("PickFinalTimer", _timeSettings.CharacterPickFinalStateTime);

            finalTimer.Elapsed += BeginLoadingState;
        }

        private void BeginLoadingState()
        {
            _gameStateProvider.SetState(EGameState.ClientLoading);
        }

        private void OnPlayerCharacterSelect(CharacterSelectMessage characterSelectMessage)
        {
            
        }

        private void OnCharacterPickAccepted(CharacterPickMessage characterPickMessage)
        {
            
        }

        protected override void OnDisposing()
        {
            _characterPickTimerProvider.Elapsed -= BeginFinalCountdown;
        }
    }
}