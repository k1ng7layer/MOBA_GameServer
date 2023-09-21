using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.GameState;
using Services.PresenterRepository;
using Systems.Abstract;
using UnityEngine;

namespace Systems
{
    public class PlayerDestinationSystem : AGameStateSystem
    {
        private readonly INetworkServerManager _networkServerManager;
        private readonly ICharacterPresenterRepository _characterPresenterRepository;

        public PlayerDestinationSystem(
            IGameStateProvider gameStateProvider, 
            INetworkServerManager networkServerManager,
            ICharacterPresenterRepository characterPresenterRepository
        ) : base(gameStateProvider)
        {
            _networkServerManager = networkServerManager;
            _characterPresenterRepository = characterPresenterRepository;
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
        }
    }
}