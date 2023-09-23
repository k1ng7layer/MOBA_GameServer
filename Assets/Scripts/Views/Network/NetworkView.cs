using System;
using PBUnityMultiplayer.Runtime.Core.NetworkObjects;
using UnityEngine;

namespace Views.Network
{
    [RequireComponent(typeof(NetworkObject))]
    public class NetworkView : MonoBehaviour, INetworkView
    {
        private NetworkObject _networkObject;
        
        private void Awake()
        {
            var hasObject = TryGetComponent<NetworkObject>(out var networkObject);

            if (!hasObject)
                throw new Exception($"[{nameof(NetworkView)}] view must have NetworkObject component");

            _networkObject = networkObject;
            
            OnAwake();
        }

        protected virtual void OnAwake()
        { }

        public int OwnerId => _networkObject.OwnerId;
        
        public int NetworkObjectId => _networkObject.Id;

    }
}