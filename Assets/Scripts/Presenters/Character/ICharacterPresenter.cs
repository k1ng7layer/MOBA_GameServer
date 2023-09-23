using UnityEngine;

namespace Presenters.Character
{
    public interface ICharacterPresenter
    {
        int CharacterNetworkId { get; }
        Vector3 Position { get; }
        Vector3 Destination { get; }
        void SetDestination(Vector3 destination);
    }
}