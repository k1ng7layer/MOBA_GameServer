using System;
using Core.Systems;
using Messages;
using Models;
using PBUdpTransport.Utils;
using PBUnityMultiplayer.Runtime.Configuration.Connection;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.CharacterPick;
using Services.GameState;
using Services.PlayerProvider;
using Services.Team;
using UnityEngine;

namespace Systems.Initialize
{
    public class InitializeServerSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly INetworkServerManager _serverManager;
        private readonly INetworkConfiguration _networkConfiguration;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly ITeamProvider _teamProvider;
        private readonly IPickProvider _pickProvider;

        public InitializeServerSystem(
            INetworkServerManager serverManager, 
            INetworkConfiguration networkConfiguration,
            IGameStateProvider gameStateProvider,
            IPlayerProvider playerProvider,
            ITeamProvider teamProvider,
            IPickProvider pickProvider
        )
        {
            _serverManager = serverManager;
            _networkConfiguration = networkConfiguration;
            _gameStateProvider = gameStateProvider;
            _playerProvider = playerProvider;
            _teamProvider = teamProvider;
            _pickProvider = pickProvider;
        }
        
        public void Initialize()
        {
            _serverManager.StartServer();
            _serverManager.ClientReady += OnClientReady;
            
            Debug.Log($"server started");
        }

        private void OnClientReady(int clientId)
        {
            Debug.Log($"OnClientReady with id = {clientId}");
            var team = _teamProvider.GetTeamType();
            var player = new Player(clientId, team);
            player.GameState = EGameState.Preparing;
            _playerProvider.AddPlayer(player);

            //var character = new CharacterDto();

            //_pickProvider.AddCharacterPick(character, clientId);

            var teamMessage = new TeamAssignMessage
            {
                teamIndex = (int)team
            };
            
            _serverManager.SendMessage(clientId, teamMessage, ESendMode.Reliable);
            
            if (_serverManager.ConnectedClients.Count == _networkConfiguration.MaxClients)
            {
               _gameStateProvider.SetState(EGameState.CharacterPick);
            }
        }

        void IDisposable.Dispose()
        {
            _serverManager.StopServer();
            _serverManager.ClientReady -= OnClientReady;
        }
    }
}