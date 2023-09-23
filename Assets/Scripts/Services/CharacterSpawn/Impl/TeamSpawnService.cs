using System;
using System.Collections.Generic;
using Messages;
using PBUnityMultiplayer.Runtime.Core.NetworkManager.Models;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.CharacterPick;
using Services.GameField;
using Services.PlayerProvider;
using Signals;
using UniRx;
using UnityEngine;
using Views.Character.Impl;

namespace Services.CharacterSpawn.Impl
{
    public class TeamSpawnService : ITeamSpawnService
    {
        private readonly INetworkServerManager _networkServerManager;
        private readonly IGameFieldProvider _gameFieldProvider;
        private readonly IPickProvider _pickProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly ReactiveCommand<List<CharacterView>> _teamSpawned = new();

        public TeamSpawnService(
            INetworkServerManager networkServerManager,
            IGameFieldProvider gameFieldProvider,
            IPickProvider pickProvider,
            IPlayerProvider playerProvider
        )
        {
            _networkServerManager = networkServerManager;
            _gameFieldProvider = gameFieldProvider;
            _pickProvider = pickProvider;
            _playerProvider = playerProvider;
        }

        public IReactiveCommand<List<CharacterView>> TeamSpawned => _teamSpawned;

        public List<CharacterView> Spawn(ETeamType type)
        {
            var spawnIndex = 0;

            var teamSettings = type == ETeamType.Red
                ? _gameFieldProvider.Field.RedTeamLevelSettings
                : _gameFieldProvider.Field.BlueTeamLevelSettings;

            var result = new List<CharacterView>();

            var team = type == ETeamType.Red ? _playerProvider.RedTeam : _playerProvider.BlueTeam;
            foreach (var player in team)
            {
                var playerId = player.Id;
                var characterDto = _pickProvider.PickTable[playerId];
                var characterId = characterDto.Id;
                
                var clients = _networkServerManager.ConnectedClients;
                var hasClient = clients.TryGetValue(playerId, out var client);

                if(!hasClient)
                    continue;
                
                var spawnTransformData = teamSettings.teamPlayersSpawnTransforms[spawnIndex];
                var spawnTransform = spawnTransformData.spawnTransform;
                
                var spawnMessage = new CharacterSpawnMessage
                {
                    CharacterId = characterId,
                    ClientId = playerId
                };
                
                var characterNetworkObject = _networkServerManager.Spawn(characterId, client, spawnTransform.position, spawnTransform.rotation, spawnMessage);
            
                spawnIndex++;

                var view = characterNetworkObject.GetComponent<CharacterView>();
                
                view.Initialize(characterDto.Id);
                
                result.Add(view);
            }

            TeamSpawned?.Execute(result);
            
            return result;
        }
    }
}