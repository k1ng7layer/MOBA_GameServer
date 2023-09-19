using System.Collections.Generic;
using Messages;
using Models;
using PBUnityMultiplayer.Runtime.Core.NetworkObjects;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.CharacterSpawn;
using Services.GameState;
using Services.PlayerProvider;
using Signals;
using Systems.Abstract;
using Views.Character.Impl;

namespace Systems
{
    public class SpawnTeamSystem : AGameStateSystem
    {
        private readonly ITeamSpawnService _teamSpawnService;
        private readonly INetworkServerManager _networkServerManager;

        public SpawnTeamSystem(
            IGameStateProvider gameStateProvider,
            ITeamSpawnService teamSpawnService,
            INetworkServerManager networkServerManager
        ) : base(gameStateProvider)
        {
            _teamSpawnService = teamSpawnService;
            _networkServerManager = networkServerManager;
        }

        public override EGameState GameState => EGameState.Game;
        
        protected override void OnStateChanged()
        {
            var redTeam = _teamSpawnService.Spawn(ETeamType.Red);
            var blueTeam = _teamSpawnService.Spawn(ETeamType.Blue);

            foreach (var character in redTeam)
            {
                var spawnMessage = new CharacterSpawnMessage
                {
                    ClientId = character.OwnerId,
                    CharacterId = character.PrefabId,
                };
                
                _networkServerManager.SendMessage(spawnMessage);
            }
            
            foreach (var character in blueTeam)
            {
                var spawnMessage = new CharacterSpawnMessage
                {
                    ClientId = character.OwnerId,
                    CharacterId = character.PrefabId,
                };
                
                _networkServerManager.SendMessage(spawnMessage);
            }
        }
    }
}