using System;
using System.Collections.Generic;
using PBUnityMultiplayer.Runtime.Core.NetworkManager.Models;
using Services.PlayerProvider;
using UniRx;
using UnityEngine;
using Views.Character.Impl;

namespace Services.CharacterSpawn
{
    public interface ITeamSpawnService
    {
        IReactiveCommand<List<CharacterView>> TeamSpawned { get; }
        List<CharacterView> Spawn(ETeamType type);
    }
}