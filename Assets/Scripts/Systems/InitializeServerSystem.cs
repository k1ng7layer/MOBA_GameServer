using System;
using Core.Systems;
using Messages;
using PBUnityMultiplayer.Runtime.Configuration.Connection;
using PBUnityMultiplayer.Runtime.Core.NetworkManager.Models;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services;
using Services.GameState;
using Services.PlayerProvider;
using Services.Team;
using UnityEngine;

namespace Systems
{
    public class InitializeServerSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly INetworkServerManager _serverManager;
        private readonly INetworkConfiguration _networkConfiguration;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly ITeamProvider _teamProvider;

        public InitializeServerSystem(
            INetworkServerManager serverManager, 
            INetworkConfiguration networkConfiguration,
            IGameStateProvider gameStateProvider,
            IPlayerProvider playerProvider,
            ITeamProvider teamProvider
        )
        {
            _serverManager = serverManager;
            _networkConfiguration = networkConfiguration;
            _gameStateProvider = gameStateProvider;
            _playerProvider = playerProvider;
            _teamProvider = teamProvider;
        }
        
        public void Initialize()
        {
            _serverManager.StartServer();
            _serverManager.ClientReadyToWork += OnClientReady;
            
            Debug.Log($"server started");
        }

        private void OnClientReady(NetworkClient networkClient)
        {
            var team = _teamProvider.GetTeamType();
            var player = new Player(networkClient.Id, team);
            
            _playerProvider.AddPlayer(player);

            var teamMessage = new TeamAssignMessage
            {
                teamIndex = (int)team
            };
            
            _serverManager.SendMessage(teamMessage, networkClient.Id);
            
            if (_serverManager.ConnectedClients.Count == _networkConfiguration.MaxClients)
            {
               _gameStateProvider.SetState(EGameState.CharacterPick);
            }
        }

        void IDisposable.Dispose()
        {
            _serverManager.StopServer();
            _serverManager.ClientReadyToWork -= OnClientReady;
        }
    }
}