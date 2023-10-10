using System.Collections.Generic;
using PBUnityMultiplayer.Runtime.Core.NetworkObjects;

namespace Services.Ownership
{
    public interface INetworkOwnershipRepository
    {
        void Add(int playerId, NetworkObject networkObject);
        bool TryGet(int playerId, out HashSet<NetworkObject> ownerShip);
    }
}