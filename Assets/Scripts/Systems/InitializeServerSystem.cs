using System;
using Core.Systems;
using PBUnityMultiplayer.Runtime.Configuration.Connection;
using PBUnityMultiplayer.Runtime.Core.NetworkManager.Models;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services;
using Services.GameState;

namespace Systems
{
    public class InitializeServerSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly INetworkServerManager _serverManager;
        private readonly INetworkConfiguration _networkConfiguration;
        private readonly IGameStateProvider _gameStateProvider;

        public InitializeServerSystem(
            INetworkServerManager serverManager, 
            INetworkConfiguration networkConfiguration,
            IGameStateProvider gameStateProvider
        )
        {
            _serverManager = serverManager;
            _networkConfiguration = networkConfiguration;
            _gameStateProvider = gameStateProvider;
        }
        
        public void Initialize()
        {
            _serverManager.StartServer();
            _serverManager.SeverAuthenticated += OnClientAuthenticated;
        }

        private void OnClientAuthenticated(NetworkClient networkClient)
        {
            if (_serverManager.ConnectedClients.Count == _networkConfiguration.MaxClients)
            {
               _gameStateProvider.SetState(EGameState.CharacterPick);
               
            }
        }

        void IDisposable.Dispose()
        {
            _serverManager.SeverAuthenticated -= OnClientAuthenticated;
        }
    }
}