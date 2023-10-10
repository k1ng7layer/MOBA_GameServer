using Factories.Character;
using Messages;
using PBUdpTransport.Utils;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.CharacterSpawn;
using Services.GameField;
using Services.GameState;
using Services.Ownership;
using Services.PlayerProvider;
using Services.PresenterRepository;
using Systems.Abstract;
using Views.Character.Impl;

namespace Systems.Game
{
    public class SpawnPlayersSystem : AGameStateSystem
    {
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly ISpawnManager _spawnManager;
        private readonly INetworkOwnershipRepository _networkOwnershipRepository;
        private readonly INetworkServerManager _serverManager;
        private readonly ICharacterPresenterRepository _characterPresenterRepository;
        private readonly CharacterFactory _characterFactory;
        private readonly CharacterPresenterFactory _characterPresenterFactory;

        public SpawnPlayersSystem(
            IGameStateProvider gameStateProvider, 
            IGameFieldProvider gameFieldProvider,
            IPlayerProvider playerProvider,
            ISpawnManager spawnManager,
            INetworkOwnershipRepository networkOwnershipRepository,
            INetworkServerManager serverManager,
            ICharacterPresenterRepository characterPresenterRepository,
            CharacterFactory characterFactory,
            CharacterPresenterFactory characterPresenterFactory
        ) : base(gameStateProvider)
        {
            _gameFieldProvider = gameFieldProvider;
            _playerProvider = playerProvider;
            _spawnManager = spawnManager;
            _networkOwnershipRepository = networkOwnershipRepository;
            _serverManager = serverManager;
            _characterPresenterRepository = characterPresenterRepository;
            _characterFactory = characterFactory;
            _characterPresenterFactory = characterPresenterFactory;
        }

        public override EGameState GameState => EGameState.Game;
        
        protected override void OnStateChanged()
        {
            var players = _playerProvider.Players;
            var spawnIndexRed = 0;
            var spawnIndexBlue = 0;
            
            foreach (var player in players)
            {
                var teamSpawnParams = player.Team == ETeam.Blue
                    ? _gameFieldProvider.Field.BlueTeamLevelSettings
                    : _gameFieldProvider.Field.RedTeamLevelSettings;

                var spawnIndex = player.Team == ETeam.Blue ? spawnIndexBlue++ : spawnIndexRed++;

                var spawnTransform = teamSpawnParams[spawnIndex];

                var spawnPosition = spawnTransform.position;
                var spawnRotation = spawnTransform.rotation;
                var networkObject = _spawnManager.Spawn(player.CharacterId, 
                    player.Id, 
                    spawnPosition, 
                    spawnRotation);
                
                
                var character = _characterFactory.Create();
                var characterView = networkObject.GetComponent<CharacterView>();
                var characterPresenter = _characterPresenterFactory.Create(characterView, character);
                var networkId = networkObject.Id;
                _characterPresenterRepository.Add(networkId, characterPresenter);
                
                _networkOwnershipRepository.Add(player.Id, networkObject);
                
                var spawnMessage = new CharacterSpawnMessage
                {
                    ClientId = player.Id,
                    CharacterId = player.CharacterId,
                    NetworkId = networkObject.Id,
                    PositionX = spawnPosition.x,
                    PositionY = spawnPosition.y,
                    PositionZ = spawnPosition.z,
                    RotationX = spawnRotation.x,
                    RotationY = spawnRotation.y,
                    RotationZ = spawnRotation.z,
                };
                
                _serverManager.SendMessage(spawnMessage, ESendMode.Reliable);
            }
        }
    }
}