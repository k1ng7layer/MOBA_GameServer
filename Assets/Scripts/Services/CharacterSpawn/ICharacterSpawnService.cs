using System;
using System.Collections.Generic;
using PBUnityMultiplayer.Runtime.Core.NetworkManager.Models;
using Services.PlayerProvider;
using UnityEngine;
using Views.Character.Impl;

namespace Services.CharacterSpawn
{
    public interface ITeamSpawnService
    {
        event Action<List<CharacterView>> TeamSpawned;
        List<CharacterView> Spawn(ETeamType type);
    }
}