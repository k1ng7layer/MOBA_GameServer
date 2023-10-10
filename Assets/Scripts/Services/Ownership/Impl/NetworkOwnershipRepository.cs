using System.Collections.Generic;
using PBUnityMultiplayer.Runtime.Core.NetworkObjects;

namespace Services.Ownership.Impl
{
    public class NetworkOwnershipRepository : INetworkOwnershipRepository
    {
        private readonly Dictionary<int, HashSet<NetworkObject>> _ownershipTable = new();
        
        public void Add(int playerId, NetworkObject networkObject)
        {
            if(!_ownershipTable.ContainsKey(playerId))
                _ownershipTable.Add(playerId, new HashSet<NetworkObject>());

            _ownershipTable[playerId].Add(networkObject);
        }

        public bool TryGet(int playerId, out HashSet<NetworkObject> ownerShip)
        {
            return _ownershipTable.TryGetValue(playerId, out ownerShip);
        }
    }
}