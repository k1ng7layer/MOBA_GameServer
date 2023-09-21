using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.GameState;
using Services.PlayerProvider;
using Systems.Abstract;
using UnityEngine;

namespace Systems
{
    public class WaitForClientLoadingSystem : AGameStateSystem
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly INetworkServerManager _networkServerManager;
        private readonly IPlayerProvider _playerProvider;

        public WaitForClientLoadingSystem(
            IGameStateProvider gameStateProvider, 
            INetworkServerManager networkServerManager,
            IPlayerProvider playerProvider) : base(gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
            _networkServerManager = networkServerManager;
            _playerProvider = playerProvider;
        }

        public override EGameState GameState => EGameState.ClientLoading;
        
        protected override void OnStateChanged()
        {
            //TODO: add loading timeout
            _networkServerManager.RegisterMessageHandler<ClientLoadingCompleteMessage>(OnClientLevelLoadingCompleted);
            var message = new ServerGameState()
            {
                gameStateId = (int)EGameState.ClientLoading
            };
            _networkServerManager.SendMessage(message);
        }

        private void OnClientLevelLoadingCompleted(ClientLoadingCompleteMessage message)
        {
            var playerId = message.ClientId;
            var hasPlayer = _playerProvider.Players.TryGetValue(playerId, out var player);
            Debug.Log($"OnClientLevelLoadingCompleted {playerId}, has = {hasPlayer}");
            if(!hasPlayer)
                return;

            player.IsLoaded = true;

            var isAllReady = IsAllPlayersReady();

            if (isAllReady)
            {
                _gameStateProvider.SetState(EGameState.Game);
                var serverGameStateMsg = new ServerGameState
                {
                    gameStateId = (int)EGameState.Game
                };
            
                _networkServerManager.SendMessage(serverGameStateMsg);
            }
              
        }

        private bool IsAllPlayersReady()
        {
            foreach (var gamePlayer in _playerProvider.Players.Values)
            {
                if (!gamePlayer.IsLoaded)
                    return false;
            }

            return true;
        }
    }
}