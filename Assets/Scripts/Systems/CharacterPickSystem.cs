using System;
using Core.Systems;
using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.CharacterPick;
using Services.GameState;
using Services.PlayerProvider;
using Systems.Abstract;
using UnityEngine;

namespace Systems
{
    public class CharacterPickSystem : AGameStateSystem, 
        IUpdateSystem
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly ICharacterPickTimerProvider _characterPickTimerProvider;
        private readonly INetworkServerManager _serverManager;
        private readonly IPlayerProvider _playerProvider;

        public CharacterPickSystem(
            IGameStateProvider gameStateProvider, 
            ICharacterPickTimerProvider characterPickTimerProvider,
            INetworkServerManager serverManager,
            IPlayerProvider playerProvider
        ) : base(gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
            _characterPickTimerProvider = characterPickTimerProvider;
            _serverManager = serverManager;
            _playerProvider = playerProvider;
        }

        public override EGameState GameState => EGameState.CharacterPick;
        
        protected override void OnStateChanged()
        {
            _characterPickTimerProvider.StartTimer();
            //_characterPickTimerProvider.Elapsed += BeginFinalCountdown;
            
            _serverManager.RegisterMessageHandler<CharacterSelectMessage>(OnPlayerCharacterSelect);
            _serverManager.RegisterMessageHandler<CharacterPickMessage>(OnCharacterPickAccepted);

            var message = new ServerGameState
            {
                gameStateId = (int)EGameState.CharacterPick
            };
            
            _serverManager.SendMessage(message);
        }

        public void Update()
        {
            // var timer = _characterPickTimerProvider.Value;
            //
            // _serverManager.SendMessage(new CharacterPickTimerMessage(timer));
        }

        private void BeginFinalCountdown()
        {
            _gameStateProvider.SetState(EGameState.PreparingAfterPick);
        }

        private void OnPlayerCharacterSelect(CharacterSelectMessage characterSelectMessage)
        {
            var playerId = characterSelectMessage.ClientId;
            var hasPlayer = _playerProvider.TryGet(playerId, out var player);
            Debug.Log($"OnPlayerCharacterSelect, playerId = {playerId}");
            if(!hasPlayer)
                return;

            var characterId = characterSelectMessage.CharacterId;

            player.CharacterId = characterId;
            
        }

        private void OnCharacterPickAccepted(CharacterPickMessage characterPickMessage)
        {
            var playerId = characterPickMessage.ClientId;
            var hasPlayer = _playerProvider.TryGet(playerId, out var player);
            Debug.Log($"OnPlayerCharacterSelect, playerId = {playerId}");
            if(!hasPlayer)
                return;

            player.CharacterLocked = true;
            player.CharacterId = characterPickMessage.CharacterId;

            var lobbyIsReady = CheckAllPlayersPicked();

            if (lobbyIsReady)
            {
                _gameStateProvider.SetState(EGameState.ClientLoading);
            }
        }

        private bool CheckAllPlayersPicked()
        {
            var players = _playerProvider.Players;

            foreach (var player in players.Values)
            {
                if (!player.CharacterLocked)
                    return false;
            }

            return true;
        }
    }
}