using PBUnityMultiplayer.Runtime.Core.NetworkObjects;
using UnityEngine;

namespace Services.CharacterSpawn
{
    public interface ISpawnManager
    {
        NetworkObject Spawn(int prefabId, int ownerId, Vector3 position, Quaternion rotation);
    }
}