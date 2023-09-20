using UnityEngine;
using Views.Character;

namespace Models
{
    public class Character
    {
        public ICharacterView CharacterView { get; }
        public string Name { get; }
        public int ID { get; }

        public void SetDestination(Vector3 destination)
        {
            CharacterView.SetDestination(destination);
        }
    }
}