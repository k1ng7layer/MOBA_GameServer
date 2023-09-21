using PBUnityMultiplayer.Runtime.Core.NetworkObjects;
using UnityEngine;
using UnityEngine.AI;
using Views.Interfaces;
using NetworkView = Views.Network.NetworkView;

namespace Views.Character.Impl
{
    public class CharacterView : NetworkView, 
        ICharacterView
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        
        public int PrefabId { get; private set; }
        
        public void SetDestination(Vector3 destination)
        {
            Debug.Log($"set destination {destination} for character id = { navMeshAgent.pathPending} ");
            //var target = new Vector3(destination.x, gameObject.transform.position.y, destination.z);
            navMeshAgent.SetDestination(destination);
        }

        public void Initialize(int prefabId)
        {
            PrefabId = prefabId;
        }
    }
}