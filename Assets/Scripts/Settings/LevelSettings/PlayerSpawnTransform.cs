using System;
using UnityEngine;

namespace Settings.LevelSettings
{
    [Serializable]
    public class PlayerSpawnTransform
    {
        public int playerId;
        public Transform spawnTransform;
    }
}