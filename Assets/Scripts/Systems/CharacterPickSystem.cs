using System;
using Core.Systems;
using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.CharacterPick;
using Services.GameState;
using Systems.Abstract;

namespace Systems
{
    public class CharacterPickSystem : AGameStateSystem, 
        IUpdateSystem
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly ICharacterPickTimerProvider _characterPickTimerProvider;
        private readonly INetworkServerManager _serverManager;

        public CharacterPickSystem(
            IGameStateProvider gameStateProvider, 
            ICharacterPickTimerProvider characterPickTimerProvider,
            INetworkServerManager serverManager
        ) : base(gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
            _characterPickTimerProvider = characterPickTimerProvider;
            _serverManager = serverManager;
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
            _gameStateProvider.SetState(EGameState.PreparingAfterPick);
        }

        private void OnPlayerCharacterSelect(CharacterSelectMessage characterSelectMessage)
        {
            
        }

        private void OnCharacterPickAccepted(CharacterPickMessage characterPickMessage)
        {
            
        }
    }
}