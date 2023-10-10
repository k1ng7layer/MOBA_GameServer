using Core.Systems;
using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.GameState;
using Services.PlayerProvider;
using Systems.Abstract;
using UnityEngine;

namespace Systems.CharacterPick
{
    public class CharacterPickSystem : AGameStateSystem, 
        IUpdateSystem
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly INetworkServerManager _serverManager;
        private readonly IPlayerProvider _playerProvider;

        public CharacterPickSystem(
            IGameStateProvider gameStateProvider,
            INetworkServerManager serverManager,
            IPlayerProvider playerProvider
        ) : base(gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
            _serverManager = serverManager;
            _playerProvider = playerProvider;
        }

        public override EGameState GameState => EGameState.CharacterPick;
        
        protected override void OnStateChanged()
        {
            _serverManager.RegisterMessageHandler<CharacterSelectMessage>(OnPlayerCharacterSelect);
            _serverManager.RegisterMessageHandler<CharacterPickMessage>(OnCharacterPickAccepted);
        }

        public void Update()
        {
        }

        private void BeginFinalCountdown()
        {
            _gameStateProvider.SetState(EGameState.PreparingAfterPick);
        }

        private void OnPlayerCharacterSelect(CharacterSelectMessage characterSelectMessage)
        {
            var playerId = characterSelectMessage.ClientId;
            var hasPlayer = _playerProvider.TryGet(playerId, out var player);
            //Debug.Log($"OnPlayerCharacterSelect, playerId = {playerId}");
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

            var lobbyIsReady = CheckAllPlayersPicked();
            
            if (lobbyIsReady)
            {
                _gameStateProvider.SetState(EGameState.ClientLoading);
            }
        }

        private bool CheckAllPlayersPicked()
        {
            var pick = _playerProvider.Players;

            foreach (var player in pick)
            {
                if (!player.CharacterLocked)
                    return false;
            }

            return true;
        }
    }
}