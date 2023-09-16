using System.Collections.Generic;
using Messages;
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

            var playerList = teamType == ETeamType.Red ? _playerProvider.RedTeam : _playerProvider.BlueTeam;
            
            var teamSettings = teamType == ETeamType.Red
                ? _gameFieldProvider.Field.RedTeamLevelSettings
                : _gameFieldProvider.Field.BlueTeamLevelSettings;
            
            foreach (var player in playerList)
            {
                var clients = _networkServerManager.ConnectedClients;
                var hasClient = clients.TryGetValue(player.Id, out var client);
                
                if(!hasClient)
                    continue;
                
                var spawnTransformData = teamSettings.teamPlayersSpawnTransforms[spawnIndex];
                var spawnTransform = spawnTransformData.spawnTransform;
                
                var spawnMessage = new CharacterSpawnMessage
                {
                    CharacterId = player.CharacterId,
                    ClientId = player.Id
                };
                _networkServerManager.Spawn(player.CharacterId, client, spawnTransform.position, spawnTransform.rotation, spawnMessage);

                spawnIndex++;
            }
        }
    }
}