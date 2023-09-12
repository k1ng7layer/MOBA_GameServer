using System;
using System.Collections.Generic;
using PBUnityMultiplayer.Runtime.Utils.Attributes;
using UnityEngine;

namespace Settings.LevelSettings
{
    [Serializable]
    public class TeamLevelSettings
    {
        [KeyValue(nameof(PlayerSpawnTransform.PlayerId))]
        public List<PlayerSpawnTransform> teamPlayersSpawnTransforms;
    }
}