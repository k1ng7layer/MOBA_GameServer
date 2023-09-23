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
        public Vector3 Position => transform.position;
        
        public void SetDestination(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }
        
        public void Initialize(int prefabId)
        {
            PrefabId = prefabId;
        }

        public void SetOwnerId(int id)
        {
            
        }
    }
}