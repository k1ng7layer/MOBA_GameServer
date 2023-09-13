using PBUnityMultiplayer.Runtime.Core.Server;
using Services.GameField;
using Services.GameState;
using Services.PlayerProvider;
using Systems.Abstract;

namespace Systems
{
    public class SpawnPlayersSystem : AGameStateSystem
    {
        private readonly IPlayerProvider _playerProvider;
        private readonly INetworkServerManager _networkServerManager;
        private readonly IGameFieldProvider _gameFieldProvider;

        public SpawnPlayersSystem(
            IGameStateProvider gameStateProvider, 
            IPlayerProvider playerProvider,
            INetworkServerManager networkServerManager,
            IGameFieldProvider gameFieldProvider
        ) : base(gameStateProvider)
        {
            _playerProvider = playerProvider;
            _networkServerManager = networkServerManager;
            _gameFieldProvider = gameFieldProvider;
        }

        public override EGameState GameState => EGameState.Game;
        
        protected override void OnStateChanged()
        {
            SpawnTeam(ETeamType.Blue);
            SpawnTeam(ETeamType.Red);
        }

        private void SpawnTeam(ETeamType teamType)
        {
            var spawnIndex = 0;
            
            var players = _playerProvider.RedTeam;
            
            var teamSettings = teamType == ETeamType.Red
                ? _gameFieldProvider.Field.RedTeamLevelSettings
                : _gameFieldProvider.Field.BlueTeamLevelSettings;
            
            foreach (var player in players)
            {
                var clients = _networkServerManager.ConnectedClients;
                var hasClient = clients.TryGetValue(player.Id, out var client);
                
                if(!hasClient)
                    continue;
                
                var spawnTransformData = teamSettings.teamPlayersSpawnTransforms[spawnIndex];
                var spawnTransform = spawnTransformData.spawnTransform;
                
                _networkServerManager.Spawn(player.CharacterId, client, spawnTransform.position, spawnTransform.rotation);

                spawnIndex++;
            }
        }
    }
}