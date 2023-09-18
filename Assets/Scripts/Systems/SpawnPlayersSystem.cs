using System.Collections.Generic;
using Messages;
using Models;
using PBUnityMultiplayer.Runtime.Core.NetworkObjects;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.CharacterPick;
using Services.GameField;
using Services.GameState;
using Services.PlayerProvider;
using Systems.Abstract;
using Views.Character.Impl;

namespace Systems
{
    public class SpawnPlayersSystem : AGameStateSystem
    {
        private readonly IPlayerProvider _playerProvider;
        private readonly INetworkServerManager _networkServerManager;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IPickProvider _pickProvider;

        public SpawnPlayersSystem(
            IGameStateProvider gameStateProvider, 
            IPlayerProvider playerProvider,
            INetworkServerManager networkServerManager,
            IGameFieldProvider gameFieldProvider,
            IPickProvider pickProvider
        ) : base(gameStateProvider)
        {
            _playerProvider = playerProvider;
            _networkServerManager = networkServerManager;
            _gameFieldProvider = gameFieldProvider;
            _pickProvider = pickProvider;
        }

        public override EGameState GameState => EGameState.Game;
        
        protected override void OnStateChanged()
        {
            SpawnTeam(ETeamType.Blue);
            SpawnTeam(ETeamType.Red);
        }

        protected override void OnInitialize()
        {
            //_networkServerManager.RegisterSpawnHandler<CharacterSpawnMessage>(OnCharacterSpawned);
        }

        private void SpawnTeam(ETeamType teamType)
        {
            var spawnIndex = 0;

            var playerList = teamType == ETeamType.Red ? _playerProvider.RedTeam : _playerProvider.BlueTeam;
            
            var teamSettings = teamType == ETeamType.Red
                ? _gameFieldProvider.Field.RedTeamLevelSettings
                : _gameFieldProvider.Field.BlueTeamLevelSettings;
            
            foreach (var characterKvp in _pickProvider.PickTable)
            {
                var playerId = characterKvp.Key;
                var characterDto = characterKvp.Value;
                var characterId = characterDto.Id;
                
                var clients = _networkServerManager.ConnectedClients;
                var hasClient = clients.TryGetValue(playerId, out var client);
                
                var player = _playerProvider.Players[playerId];
                
                if(!hasClient)
                    continue;
                
                var spawnTransformData = teamSettings.teamPlayersSpawnTransforms[spawnIndex];
                var spawnTransform = spawnTransformData.spawnTransform;
                
                var spawnMessage = new CharacterSpawnMessage
                {
                    CharacterId = characterId,
                    ClientId = playerId
                };
                
                var characterNetworkObject = _networkServerManager.Spawn(characterKvp.Value.Id, client, spawnTransform.position, spawnTransform.rotation, spawnMessage);
            
                spawnIndex++;
                
                OnCharacterSpawned(characterNetworkObject, characterDto, player);
            }
        }

        private void OnCharacterSpawned(NetworkObject networkObject,
            CharacterDto characterDto, Player player)
        {
            var view = networkObject.GetComponent<CharacterView>();

            var character = new Character(view, characterDto.Name, characterDto.Id);
            
            player.SetCharacter(character);
        }
    }
}