using UnityEngine;
using Views.Interfaces;
using Views.Network;

namespace Views.Character
{
    public interface ICharacterView : IAiView, INetworkView
    {
        Vector3 Position { get; }
        void Initialize(int prefabId);
    }
}