using UnityEngine;
using Views.Character;

namespace Models
{
    public class Character
    {
        public Vector3 Position { get; private set; }
        public Vector3 Destination { get; private set; }
        public string Name { get; }
        public int ID { get; }

        public void SetDestination(Vector3 destination)
        {
            Destination = destination;
        }
        
        public void SetPosition(Vector3 position)
        {
            Position = position;
        }
    }
}