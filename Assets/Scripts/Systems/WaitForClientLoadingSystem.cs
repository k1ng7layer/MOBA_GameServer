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

        protected override void OnInitialize() 
        {
            _networkServerManager.RegisterMessageHandler<ClientLoadingCompleteMessage>(OnClientLevelLoadingCompleted);
        }

        protected override void OnStateChanged()
        {
            //TODO: add loading timeout
           
            // var message = new ServerGameState()
            // {
            //     gameStateId = (int)EGameState.ClientLoading
            // };
            // _networkServerManager.SendMessage(message);
        }

        private void OnClientLevelLoadingCompleted(ClientLoadingCompleteMessage message)
        {
            var playerId = message.clientId;
            var hasPlayer = _playerProvider.PlayersTable.TryGetValue(playerId, out var player);
            Debug.Log($"OnClientLevelLoadingCompleted {playerId}, has = {hasPlayer}");
            if(!hasPlayer)
                return;

            player.IsLoaded = true;

            var isAllReady = IsAllPlayersReady();

            if (isAllReady)
            {
                Debug.Log($"all clients is ready");
                _gameStateProvider.SetState(EGameState.Game);
            }
              
        }

        private bool IsAllPlayersReady()
        {
            foreach (var gamePlayer in _playerProvider.Players)
            {
                if (!gamePlayer.IsLoaded)
                    return false;
                
                Debug.Log($"gamePlayer id {gamePlayer.Id} is loaded = {gamePlayer.IsLoaded}");
            }

            return true;
        }
    }
}