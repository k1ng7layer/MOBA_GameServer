using Messages;
using PBUdpTransport.Utils;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.GameState;
using Services.PlayerProvider;
using Services.PresenterRepository;
using Systems.Abstract;
using UnityEngine;

namespace Systems
{
    public class PlayerDestinationSystem : AGameStateSystem
    {
        private readonly INetworkServerManager _networkServerManager;
        private readonly ICharacterPresenterRepository _characterPresenterRepository;
        private readonly IPlayerProvider _playerProvider;

        public PlayerDestinationSystem(
            IGameStateProvider gameStateProvider, 
            INetworkServerManager networkServerManager,
            ICharacterPresenterRepository characterPresenterRepository,
            IPlayerProvider playerProvider
        ) : base(gameStateProvider)
        {
            _networkServerManager = networkServerManager;
            _characterPresenterRepository = characterPresenterRepository;
            _playerProvider = playerProvider;
        }

        public override EGameState GameState => EGameState.Game;
        
        protected override void OnStateChanged()
        {
            _networkServerManager.RegisterMessageHandler<PlayerDestinationMessage>(OnPlayerDestinationRequestReceived);
        }

        private void OnPlayerDestinationRequestReceived(PlayerDestinationMessage message)
        {
            var objectId = message.networkObjectId;
            var destination = new Vector3(message.x, message.y, message.z);

            var hasPresenter = _characterPresenterRepository.TryGetPresenter(objectId, out var presenter);
            
            if(!hasPresenter)
                return;
            
            presenter.SetDestination(destination);

            //send destination to all players
            foreach (var player in _playerProvider.Players)
            {
                Debug.Log($"send destination to {player.Id}");
                _networkServerManager.SendMessage( player.Id, message, ESendMode.Reliable);
            }
          
        }
    }
}