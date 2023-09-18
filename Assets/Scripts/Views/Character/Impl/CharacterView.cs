using UnityEngine;
using UnityEngine.AI;
using Views.Interfaces;

namespace Views.Character.Impl
{
    public class CharacterView : MonoBehaviour, 
        ICharacterView
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        
        public void SetDestination(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
        }
    }
}