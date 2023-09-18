using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.GameState;
using Services.PlayerProvider;
using Systems.Abstract;
using UnityEngine;

namespace Systems
{
    public class PlayerDestinationSystem : AGameStateSystem
    {
        private readonly INetworkServerManager _networkServerManager;
        private readonly IPlayerProvider _playerProvider;

        public PlayerDestinationSystem(
            IGameStateProvider gameStateProvider, 
            INetworkServerManager networkServerManager,
            IPlayerProvider playerProvider
        ) : base(gameStateProvider)
        {
            _networkServerManager = networkServerManager;
            _playerProvider = playerProvider;
        }

        public override EGameState GameState => EGameState.Game;
        
        protected override void OnStateChanged()
        {
            _networkServerManager.RegisterMessageHandler<PlayerDestinationMessage>(OnPlayerDestinationRequestReceived);
        }

        private void OnPlayerDestinationRequestReceived(PlayerDestinationMessage message)
        {
            var playerId = message.PlayerId;

            var hasPlayer = _playerProvider.TryGet(playerId, out var player);
            
            if(!hasPlayer)
                return;

            var destination = new Vector3(message.X, message.Y, message.Z);
            
            player.Character.SetDestination(destination);
        }
    }
}