﻿using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.GameState;
using Services.PlayerProvider;
using Systems.Abstract;

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

        protected override EGameState GameState => EGameState.ClientLoading;
        
        protected override void OnGameStateChanged()
        {
            //TODO: add loading timeout
            _networkServerManager.RegisterMessageHandler<ClientLoadingCompleteMessage>(OnClientLevelLoadingCompleted);
        }

        private void OnClientLevelLoadingCompleted(ClientLoadingCompleteMessage message)
        {
            var playerId = message.ClientId;
            var hasPlayer = _playerProvider.Players.TryGetValue(playerId, out var player);
            
            if(!hasPlayer)
                return;

            player.IsLoaded = true;

            var isAllReady = IsAllPlayersReady();
            
            if(isAllReady)
                _gameStateProvider.SetState(EGameState.Game);
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