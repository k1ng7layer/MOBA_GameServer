using System;

namespace Messages
{
    [Serializable]
    public struct PlayerDestinationMessage
    {
        public int PlayerId;
        public float X;
        public float Y;
        public float Z;
    }
}