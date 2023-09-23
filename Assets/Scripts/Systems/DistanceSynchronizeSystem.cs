using Core.Systems;
using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.GameState;
using Services.PresenterRepository;
using Systems.Abstract;

namespace Systems
{
    public class SynchronizePositionSystem : IFixedSystem
    {
        private readonly IGameStateProvider _gameStateProvider;
        private readonly ICharacterPresenterRepository _characterPresenterRepository;
        private readonly INetworkServerManager _networkServerManager;

        public SynchronizePositionSystem(
            IGameStateProvider gameStateProvider, 
            ICharacterPresenterRepository characterPresenterRepository,
            INetworkServerManager networkServerManager
        )
        {
            _gameStateProvider = gameStateProvider;
            _characterPresenterRepository = characterPresenterRepository;
            _networkServerManager = networkServerManager;
        }
        
        public void Fixed()
        {
            if(_gameStateProvider.CurrentState != EGameState.Game)
                return;

            var characters = _characterPresenterRepository.CharacterPresenters;

            if(_networkServerManager.Tick % 5 != 0)
                return;
            
            foreach (var characterPresenter in characters)
            {
                var position = characterPresenter.Position;
                var destination = characterPresenter.Destination;

                var syncMessage = new PositionSyncMessage
                {
                    networkObjId = characterPresenter.CharacterNetworkId,
                    destinationX = destination.x,
                    destinationY = destination.y,
                    destinationZ = destination.z,
                    actualX = position.x,
                    actualY = position.y,
                    actualZ = position.z,
                };
                
                _networkServerManager.SendMessage(syncMessage);
            }
        }
    }
}