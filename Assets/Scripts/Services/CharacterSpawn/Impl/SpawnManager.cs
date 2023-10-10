using PBUnityMultiplayer.Runtime.Configuration.Prefabs;
using PBUnityMultiplayer.Runtime.Core.NetworkObjects;
using PBUnityMultiplayer.Runtime.Utils.IdGenerator;
using UnityEngine;

namespace Services.CharacterSpawn.Impl
{
    public class SpawnManager : ISpawnManager
    {
        private readonly INetworkPrefabsBase _networkPrefabsBase;
        private readonly IIdGenerator<ushort> _idGenerator;

        public SpawnManager(
            INetworkPrefabsBase networkPrefabsBase, 
            IIdGenerator<ushort> idGenerator)
        {
            _networkPrefabsBase = networkPrefabsBase;
            _idGenerator = idGenerator;
        }
        
        public NetworkObject Spawn(int prefabId, int ownerId, Vector3 position, Quaternion rotation)
        {
            var prefab = _networkPrefabsBase.Get(prefabId);

            var spawned = Object.Instantiate(prefab, position, rotation);
            spawned.Spawn(_idGenerator.Next(), ownerId, false);
            
            return spawned;
        }
    }
}